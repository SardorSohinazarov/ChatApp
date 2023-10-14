const storage = window.localStorage;

window.getFromStorage = (key) => storage.getItem(key);

window.setToStorage = (key, value) => storage.setItem(key, value);

window.removeFromStorage = (key) => storage.removeItem(key);


function scrollFunction() {
    const item = document.querySelector(".items-container")
    item.scrollTop = item.scrollHeight - item.clientHeight
    console.log("run is js code")
    alert("ishladi")
}