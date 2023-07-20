const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

document.getElementById("search-library").addEventListener("click", event => {
    document.getElementById("search-input").style.display = "block";
});
document.addEventListener("click", function (e) {
    var container = document.getElementById("search-library");
    if (!container.contains(e.target)) {
        document.getElementById("search-input").style.display = "none";
    }
});