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
    const response = await fetch(THREAD_API + "/DeleteMsgById/" + msgId, {
        method: 'DELETE'
    });
    //const final = await response.json();
    //return final;
};

// delete rply by id
const deleteRplyById = async (rplyId) => {
    const response = await fetch(THREAD_API + "/DeleteRplyById/" + rplyId, {
        method: 'DELETE'
    });
    //const final = await response.json();
    //return final;
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
    // refresh UI
    // reset modal
    e.preventDefault();
    const target = e.target;
    const msgType = getEditModalDataType();
    const msgBody = document.getElementById("editMsgBody").value;
    if (msgType.toUpperCase() == "REPLY") {
        const rplyId = target.getAttribute("data-value");
        const msgData = {
            MsgBody: msgBody
        };
        const response = await editRplyById(rplyId, msgData);
        // fetch updated replies
        const rplyList = await getRplyHistory();
        rebuildRepliesTable(rplyList);
    } else {
        const msgId = target.getAttribute("data-value");
        const msgTitle = document.getElementById("editMsgTitle").value;
        const msgData = {
            MsgBody: msgBody,
            MsgTitle: msgTitle
        };
        const response = await editMsgbyId(msgId, msgData);
        // fetch updated messages
        const msgList = await getMsgHistory();
        rebuildMessagesTable(msgList);
    }
    clearEditModalInputs();
};

const deleteMsgEvent = async (e) => {
    // parse form data
    // determine data type
    // send delete request with id to api
    // refresh UI
    // reset modal
    e.preventDefault();
    const target = e.target.parentElement;
    const msgType = target.getAttribute("data-msg-type");
    if (msgType.toUpperCase() == "REPLY") {
        const rplyId = target.getAttribute("data-value");
        // fetch data
        const replyData = await getReply(rplyId);
        if (confirm("Are You Sure You Want to Delete this Reply, which says " + replyData.replyContent)) {
            await deleteRplyById(rplyId);
            const rplyHistory = await getRplyHistory();
            rebuildRepliesTable(rplyHistory);
        }
    } else {
        const msgId = target.getAttribute("data-value");
        // fetch data
        const msgData = await getMsg(msgId);
        if (confirm("Are You Sure You Want to Delete this Message, titled " + msgData.messageTitle)) {
            await deleteMsgById(msgId);
            const msgHistory = await getMsgHistory();
            rebuildMessagesTable(msgHistory);
        }
    }
    
};

// UI RENDERING FUNCTIONS
const rebuildRepliesTable = (replyList) => {
    const replyTable = document.getElementById("replyTableBody");
    const tableHTML = replyList.map((reply, index) =>
        `<tr>
            <th scope="row">${reply.replyID}</th>
            <td class="titleLimit">${reply.replyContent}</td>
            <td>${reply.getTimePosted}</td>
            <td><a data-toggle="modal" data-target="#viewRplyModal" class="view " href="#" data-msg-type="reply" data-value=${reply.replyID} onclick="viewMsgEvent(event)"><i class="fa fa-eye"></i></a></td>
            <td><a data-toggle="modal" data-target="#editModal" class="edit " href="#" data-msg-type="reply" data-value=${reply.replyID} onclick="editMsgEvent(event)"><i class="fa fa-edit"></i></a></td>
            <td><a class="trash " href="#" data-msg-type="reply" data-value=${reply.replyID}><i class="fa fa-trash"></i></a></td>
        </tr>`
    );
    replyTable.innerHTML = tableHTML.join('');
};

const rebuildMessagesTable = (msgList) => {
    const msgTable = document.getElementById("messageTableBody");
    const tableHTML = msgList.map((msg, index) =>
        `<tr>
            <th scope="row">${msg.messageID}</th>
            <td class="titleLimit">${msg.messageTitle}</td>
            <td>${msg.getTimePosted}</td>
            <td><a data-toggle="modal" data-target="#viewMsgModal" class="view" href="#" data-msg-type="message" data-value=${msg.messageID} onclick="viewMsgEvent(event)"><i class="fa fa-eye"></i></a></td>
            <td><a data-toggle="modal" data-target="#editModal" class="edit " href="#" data-msg-type="message" data-value=${msg.messageID} onclick="editMsgEvent(event)"><i class="fa fa-edit"></i></a></td>
            <td><a class="trash " href="#" data-msg-type="message" data-value=${msg.messageID}><i class="fa fa-trash"></i></a></td>
        </tr>`
    );
    msgTable.innerHTML = tableHTML.join('');
};

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
    const titleHeader = document.getElementById("editModalTitle");
    titleHeader.innerHTML = headerText;
};

const clearEditModalInputs = () => {
    document.getElementById("editMsgBody").value = "";
    document.getElementById("editMsgTitle").value = "";
};

// update button value setters (responsible for priming the data type and msgId values)
const setEditModalDataType = (typeString) => {
    document.getElementById("editModal-updateButton").value = typeString;
};

const getEditModalDataType = () => {
    return document.getElementById("editModal-updateButton").value;
};

const setEditModalMsgData = (msgId) => {
    document.getElementById("editModal-updateButton").setAttribute("data-value", msgId);
};

const getEditModalMsgData = () => {
    return document.getElementById("editModal-updateButton").getAttribute("data-value");
};



/*---------------------------------------- BEGIN MANAGE CHATS methods ---------------------------------------------*/
const deleteChatEvent = async (e) => {
    const target = e.target.parentElement;
    //const chatId = target.getAttribute("data-chatRoomID");
    /*fetch(target.href, {
        method: 'delete',
    });*/
    //target.href = "";
    //fetch("https://localhost:44387/" + "Admin" + "/DeleteChatRoom?chatRoomID=" + "6", {
    await fetch(target.href, {
        method: 'delete',
    });
};