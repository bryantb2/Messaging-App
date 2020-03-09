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

const getMsgHistory = async () => {
    const response = await fetch(THREAD_API + "/GetMessageHistory");
    const final = await response.json();
    return final;
}

const getRplyHistory = async () => {
    const response = await fetch(THREAD_API + "/GetReplyHistory");
    const final = await response.json();
    return final;
}

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
    // fetch appropriate messaging data
    // set display to none on modal title if reply
    // set modal data
    e.preventDefault();
    const target = e.target;
    const dataType = target.getAttribute("data-type");
    const msgId = target.value;

    const msgTitleGroup = document.getElementById("replyViewGroup");
    const msgTitleElement = document.getElementById("msgTitleView");
    const msgContentElement = document.getElementById("msgBodyView");

    if (dataType.toUpperCase() == "REPLY") {
        const replyData = await getReply(msgId);
        msgTitleGroup.classList.add("hideTitleInput");
        msgContentElement.value = replyData.replyContent;
    } else {
        const msgData = await getMsg(msgId);
        //msgTitleElement.value = msgData.msgTitle

    }
};

const deleteMsgEvent = async (e) => {

};

const editMsgEvent = async (e) => {

};

// UI RENDERING FUNCTIONS



















/*---------------------------------------- END MANAGE MESSAGES methods ---------------------------------------------*/