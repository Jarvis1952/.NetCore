document.querySelector("#load").addEventListener("click",
    async function () {
    var response = await fetch("emplist", { method : "GET" });
    var responsebody = await response.text();
    document.querySelector("#list").innerHTML = responsebody;
});