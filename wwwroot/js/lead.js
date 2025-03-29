
document.getElementById('searchInput').addEventListener('keyup', function () {
    var searchValue = this.value.toLowerCase();
    var tableRows = document.querySelectorAll('#leadsTable tbody tr');

    tableRows.forEach(function (row) {
        var rowText = row.textContent.toLowerCase();
        if (rowText.includes(searchValue)) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
});

document.getElementById('statusFilter').addEventListener('change', function () {
    var filterValue = this.value;
    var tableRows = document.querySelectorAll('#leadsTable tbody tr');

    tableRows.forEach(function (row) {
        var rowStatus = row.getAttribute('data-status');
        if (filterValue === 'all' || rowStatus === filterValue) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
});
function toggleCheckboxes(source) {
    let checkboxes = document.querySelectorAll('.checkbox');
    checkboxes.forEach(checkbox => {
        checkbox.checked = source.checked;
    });
}
document.getElementById('deleteButton').addEventListener('click', function () {
    const checkboxes = document.querySelectorAll('.checkbox:checked');
    checkboxes.forEach(checkbox => {
        const row = checkbox.closest('tr');
        if (row) {
            row.remove();
        }
    });
});
