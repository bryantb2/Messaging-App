const THREAD_API = "https://localhost:44387/api/ForumAPI";

/*---------------------------------------- API methods ---------------------------------------------*/

// get reply by id
const getReply = async (rplyId) => {
    const response = await fetch(THREAD_API + "/GetRplyById/" + rplyId);
    const final = await response.json();
    return final;
};

// get message by id
const getMsg = async (msgId) => {
    const response = await fetch(THREAD_API + "/GetMsgById/" + msgId);
    const final = await response.json();
    return final;
};

// get user msg history
const getMsgHistory = async () => {
    const response = await fetch(THREAD_API + "/GetMessageHistory");
    const final = await response.json();
    return final;
}

// get msg that reply responded to
const getMsgByRplyId = async (rplyId) => {
    const response = await fetch(THREAD_API + "/GetMsgByRplyId/" + rplyId);
    const final = await response.json();
    return final;
}

// get user rply history
const getRplyHistory = async () => {
    const response = await fetch(THREAD_API + "/GetReplyHistory");
    const final = await response.json();
    return final;
}

// get chatroom location of msg
const getChatRoomByMsgId = async (msgId) => {
    const response = await fetch(THREAD_API + "/GetChatRoomByMsgId/" + msgId);
    const final = await response.json();
    return final; 
}

// edit msg by id
const editMsgbyId = async (msgId, msgUpdateData) => {
    const response = await fetch(THREAD_API + "/EditMsgById/" + msgId, {
        method: 'PUT',
        body: JSON.stringify(msgUpdateData),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const final = await response.json();
    return final;
}

const editRplyById = async (rplyId, msgUpdateData) => {
    const response = await fetch(THREAD_API + "/EditRplyById/" + rplyId, {
        method: 'PUT',
        body: JSON.stringify(msgUpdateData),
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const final = await response.json();
    return final;
}

// delete msg by id
const deleteMsgById = async (msgId) => {
    const response = await fetch(THREAD_API + "/DeleteMsgById/" + msgId);
    const final = await response.json();
    return final;
};


/*---------------------------------------- Forum methods ---------------------------------------------*/

// EVENT HANDLERS
const replyEvent = async (e) => {
    // fetch message by id
    // set asp-route msgId
    // set message info the user wants to respond to
    e.preventDefault();
    const msgId = e.target.value;
    const msg = await getMsg(msgId);
    setMsgFormId(msgId);
    setReplyToContent(msg.messageTitle, msg.messageContent, msg.poster);
};

// UI RENDERING FUNCTIONS
const setReplyToContent = (msgTitle, msgBody, msgPoster) => {
    // set response reply title
    // set response reply content
    document.getElementById("replyResponseTitle").innerHTML = `Responding to <b>${msgPoster}</b>'s message titled "${msgTitle}", which says:`;
    document.getElementById("replyResponseContent").innerHTML = msgBody;
};

// ACTION PARAMETER MANIPULATION
const setMsgFormId = (msgId) => {
    const formElement = document.getElementById("replyForm");
    const formAction = formElement.action;
    let splitActionStr = formAction.split("&");

    // split action string
    splitActionStr.forEach((actionString, index) => {
        // check if element includes msgId parameter
        if (actionString.includes("msgId")) {
            // reset element with new msgId parameter
            splitActionStr[index] = "msgId=" + msgId;
        }
    });

    // rebuild action string
    let rebuiltActionStr = "";
    splitActionStr.forEach((actionElement, index) => {
        rebuiltActionStr += actionElement;
        if (index < (splitActionStr.length-1))
            rebuiltActionStr += "&";
    });

    // set action string
    formElement.action = rebuiltActionStr;
};


/*---------------------------------------- BEGIN MANAGE MESSAGES methods ---------------------------------------------*/

// EVENT HANDLERS
const viewMsgEvent = async (e) => {
    // check data-type attribute on event
    // fetch appropriate messaging and related data
    // set modal UI data
    e.preventDefault();
    const target = e.target.parentElement;
    const dataType = target.getAttribute("data-msg-type");

    if (dataType.toUpperCase() == "REPLY") {
        const rplyId = target.getAttribute("data-value");
        // get elements
        const rplyPostingInfo = document.getElementById("replyResponseTitle");
        const parentMsgContentElement = document.getElementById("replyResponseContent");
        const rplyContentElement = document.getElementById("replyBody");
        // fetch data
        const replyData = await getReply(rplyId);
        const parentMsg = await getMsgByRplyId(rplyId);
        // set vals
        rplyPostingInfo.innerHTML = `Responding to <b>${parentMsg.poster}</b>'s message titled "${parentMsg.messageTitle}", which says:`;
        parentMsgContentElement.innerHTML = parentMsg.messageContent;
        rplyContentElement.value = replyData.replyContent;
    } else {
        const msgId = target.getAttribute("data-value");
        // get elements
        const msgPostingInfo = document.getElementById("viewMsgModalInfo");
        const msgTitleElement = document.getElementById("msgTitleView");
        const msgContentElement = document.getElementById("msgBodyView");
        // fetch data
        const msgData = await getMsg(msgId);
        const locatedChat = await getChatRoomByMsgId(msgId);
        // set vals
        msgTitleElement.value = msgData.messageTitle;
        msgPostingInfo.innerHTML = `Posted to <b>${locatedChat.chatName} Chat</b> on ${msgData.getTimePosted}`;
        msgContentElement.value = msgData.messageContent;
    }
};

const deleteMsgEvent = async (e) => {

};

const editMsgEvent = async (e) => {
    // check data-type attribute on event
    // fetch appropriate messaging and related data
    // set modal UI data
    e.preventDefault();
    const target = e.target.parentElement;
    const dataType = target.getAttribute("data-msg-type");

    // get elements
    const messageBodyElement = document.getElementById("editMsgBody");
    const messageTitleElement = document.getElementById("editMsgTitle");
    if (dataType.toUpperCase() == "REPLY") {
        const rplyId = target.getAttribute("data-value");
        // fetch data
        const replyData = await getReply(rplyId);
        // set vals
        messageBodyElement.value = replyData.replyContent;
        hideEditTitleInput();
        setEditTitleHeader("Edit Reply");
        // set form data type
        setEditModalDataType("REPLY");
        setEditModalMsgData(rplyId);
    } else {
        const msgId = target.getAttribute("data-value");
        // fetch data
        const msgData = await getMsg(msgId);
        // set vals
        messageTitleElement.value = msgData.messageTitle;
        messageBodyElement.value = msgData.messageContent;
        showEditTitleInput();
        setEditTitleHeader("Edit Message");
        // set form data type
        setEditModalDataType("MESSAGE");
        setEditModalMsgData(msgId);
    }
};

const submitEditedMsgEvent = async (e) => {
    // parse form data
    // determine data type
    // send data to API
    // reset UI
    e.preventDefault();
    const msgType = getEditModalDataType();
    const msgBody = document.getElementById("editMsgBody").value;
    if (msgType.toUpperCase() == "REPLY") {
        const rplyId = target.getAttribute("data-value");
        const msgData = {
            MsgBody: msgBody
        };
        await editRplyById(rplyId, msgData);
    } else {
        const msgId = target.getAttribute("data-value");
        const msgTitle = document.getElementById("editMsgTitle").value;
        const msgData = {
            MsgBody: msgBody,
            MsgTitle: msgTitle
        };
        await editMsgbyId(rplyId, msgData);
    }
    clearEditModalInputs();
};

// UI RENDERING FUNCTIONS
const hideEditTitleInput = () => {
    const messageTitleContainer = document.getElementById("editTitleGroup");
    messageTitleContainer.classList.remove("showTitleInput");
    messageTitleContainer.classList.add("hideTitleInput");
};

const showEditTitleInput = () => {
    const messageTitleContainer = document.getElementById("editTitleGroup");
    messageTitleContainer.classList.remove("hideTitleInput");
    messageTitleContainer.classList.add("showTitleInput");
};

const setEditTitleHeader = (headerText) => {
    const titleHeader = document.getElementById("editModal-title");
    titleHeader.innerHTML = headerText;
};

const clearEditModalInputs = () => {
    document.getElementById("editMsgBody").value = "";
    document.getElementById("editMsgTitle").value = "";
};

// update button value setters
const setEditModalDataType = (typeString) => {
    document.getElementById("editModal-updateButton").value = typeString;
};

const getEditModalDataType = () => {
    return document.getElementById("editModal-updateButton").value;
};

const setEditModalMsgData = (msgId) => {
    document.getElementById("editModal-updateButton").setAttribute("data-value").value = msgId;
};

const getEditModalMsgData = () => {
    return document.getElementById("editModal-updateButton").getAttribute("data-value").value
};








