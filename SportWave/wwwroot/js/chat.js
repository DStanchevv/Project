addEventListener("load", async () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .build();

    const messagesList = document.getElementById("messagesList");
    const messageInput = document.getElementById("messageInput");
    const sendButton = document.getElementById("sendButton");

    sendButton.disabled = true;

    const scrollMessageBox = () => requestAnimationFrame(() => setTimeout(() => messagesList.scrollTo(0, messagesList.scrollHeight), 0));

    const getFormattedDate = date =>
        `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;

    const onRecieveMessage = (user, message) => {
        if (message != null && message.trim().length != 0) {
            const div1 = document.createElement("div"),
                div2 = document.createElement("div"),
                p1 = document.createElement("p"),
                p2 = document.createElement("p"),
                label1 = document.createElement("label");

            div1.classList.add("d-flex", "flex-row", "justify-content-start");
            p1.classList.add("small", "p-2", "ms-3", "mb-1", "rounded-3");
            p1.style.backgroundColor = "#f5f6f7";
            p2.classList.add("small", "ms-3", "mb-3", "rounded-3", "text-muted");
            label1.classList.add("fw-bold");

            p1.innerText = message;
            p2.innerText = getFormattedDate(new Date());
            label1.innerText = user;

            div1.appendChild(label1);
            div2.appendChild(p1);
            div2.appendChild(p2);
            div1.appendChild(div2);


            messagesList.appendChild(div1);

            scrollMessageBox();
        }
    };

    const onSendMessage = async event => {
        event.stopPropagation();
        const message = messageInput.value;
        messageInput.value = "";
        try {
            await connection.invoke("SendMessage", message);
        } catch (e) {
            alert(e);
        }
    };

    sendButton.addEventListener("click", onSendMessage);
    connection.on("RecieveMessage", onRecieveMessage);

    try {
        await connection.start();
        scrollMessageBox();
        sendButton.disabled = false;
    } catch (e) {
        console.error(e);
    }
});