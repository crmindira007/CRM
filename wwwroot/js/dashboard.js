
function toggleDropdown() {
    const dropdown = document.getElementById('add-dropdown');
    dropdown.classList.toggle('hidden');
}
window.onclick = function (event) {
    if (!event.target.matches('#add-button')) {
        const dropdowns = document.getElementsByClassName("dropdown");
        for (let i = 0; i < dropdowns.length; i++) {
            const openDropdown = dropdowns[i];
            if (!openDropdown.classList.contains('hidden')) {
                openDropdown.classList.add('hidden');
            }
        }
    }
}
// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('#add-button')) {
        const dropdowns = document.getElementsByClassName("dropdown");
        for (let i = 0; i < dropdowns.length; i++) {
            const openDropdown = dropdowns[i];
            if (!openDropdown.classList.contains('hidden')) {
                openDropdown.classList.add('hidden');
            }
        }
    }
}
function toggleSidebar() {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('expanded');

    // Adjust the main content margin based on sidebar state
    var mainContent = document.querySelector('.main-content');
    if (sidebar.classList.contains('expanded')) {
        mainContent.style.marginLeft = '200px'; // Adjust for expanded sidebar
    } else {
        mainContent.style.marginLeft = '70px'; // Adjust for collapsed sidebar
    }
}

const body = document.querySelector('body');
const btn = document.querySelector('.btn');
const icon = document.querySelector('.btn__icon');

function store(value) {
    localStorage.setItem('darkmode', value);
}

function load() {
    const darkmode = localStorage.getItem('darkmode');
    if (!darkmode) {
        store(false);
        icon.classList.add('fa-sun');
    } else if (darkmode == 'true') {
        body.classList.add('darkmode');
        icon.classList.add('fa-moon');
    } else if (darkmode == 'false') {
    }
}

load();

btn.addEventListener('click', () => {
    body.classList.toggle('darkmode');
    icon.classList.add('animated');
    store(body.classList.contains('darkmode'));

    if (body.classList.contains('darkmode')) {
        icon.classList.remove('fa-sun');
        icon.classList.add('fa-moon');
    } else {
        icon.classList.remove('fa-moon');
        icon.classList.add('fa-sun');
    }

    setTimeout(() => {
        icon.classList.remove('animated');
    }, 500);
});
document.addEventListener("DOMContentLoaded", function () {
    var calendarEl = document.getElementById("calendar");
    var storedEvents = JSON.parse(localStorage.getItem("calendarEvents")) || [];

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: "dayGridMonth",
        editable: true,
        selectable: true,
        events: storedEvents,

        dateClick: function (info) {
            Swal.fire({
                title: "Add New Event",
                html: `
                    <input id="event-title" class="swal2-input" placeholder="Event Title">
                    <input id="event-desc" class="swal2-input" placeholder="Event Description">
                    <input type="color" id="event-color" class="swal2-input" value="#007bff">
                `,
                showCancelButton: true,
                confirmButtonText: "Save",
                preConfirm: () => {
                    return {
                        title: document.getElementById("event-title").value,
                        description: document.getElementById("event-desc").value,
                        color: document.getElementById("event-color").value
                    };
                }
            }).then((result) => {
                if (result.value && result.value.title.trim() !== "") {
                    var newEvent = {
                        title: result.value.title,
                        start: info.dateStr,
                        description: result.value.description,
                        backgroundColor: result.value.color,
                        textColor: "#fff"
                    };
                    calendar.addEvent(newEvent);
                    saveEventsToLocalStorage();
                }
            });
        },

        eventClick: function (info) {
            Swal.fire({
                title: "Edit Event",
                html: `
                    <input id="edit-title" class="swal2-input" value="${info.event.title}">
                    <input id="edit-desc" class="swal2-input" value="${info.event.extendedProps.description || ''}">
                    <input type="color" id="edit-color" class="swal2-input" value="${info.event.backgroundColor}">
                `,
                showCancelButton: true,
                confirmButtonText: "Update",
                preConfirm: () => {
                    return {
                        title: document.getElementById("edit-title").value,
                        description: document.getElementById("edit-desc").value,
                        color: document.getElementById("edit-color").value
                    };
                }
            }).then((result) => {
                if (result.value) {
                    info.event.setProp("title", result.value.title);
                    info.event.setExtendedProp("description", result.value.description);
                    info.event.setProp("backgroundColor", result.value.color);
                    saveEventsToLocalStorage();
                }
            });
        },

        eventDidMount: function (info) {
            info.el.style.backgroundColor = info.event.backgroundColor;
            info.el.style.color = "#fff";
        }
    });

    calendar.render();

    function saveEventsToLocalStorage() {
        var events = calendar.getEvents().map(event => ({
            title: event.title,
            start: event.startStr,
            description: event.extendedProps.description || "",
            backgroundColor: event.backgroundColor,
            textColor: "#fff"
        }));
        localStorage.setItem("calendarEvents", JSON.stringify(events));
    }
});
function removeCard(element) {
    const card = element.closest('.card_1, .card_2, .card_3, .card_4');
    if (card) {
        card.remove();
    }
}