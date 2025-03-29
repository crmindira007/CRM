
function toggleEditMode() {
    const isEditing = document.getElementById('editMode').value === 'true';

    // Toggle display of elements
    document.querySelectorAll('[id$="Display"]').forEach(el => {
        el.style.display = isEditing ? 'block' : 'none';
    });

    document.querySelectorAll('[id$="Input"]').forEach(el => {
        el.style.display = isEditing ? 'none' : 'block';
    });

    // If switching to edit mode, update the button text
    const editButton = document.getElementById('editButton');
    if (isEditing) {
        // Save the values back to display
        document.getElementById('nameDisplay').innerText = document.getElementById('nameInput').value;
        document.getElementById('accountNameDisplay').innerText = document.getElementById('accountNameInput').value;
        document.getElementById('emailDisplay').innerText = document.getElementById('emailInput').value;
        document.getElementById('phoneDisplay').innerText = document.getElementById('phoneInput').value;
        document.getElementById('titleDisplay').innerText = document.getElementById('titleInput').value;
        document.getElementById('websiteDisplay').innerText = document.getElementById('websiteInput').value;
        document.getElementById('addressDisplay').innerText = document.getElementById('addressInput').value;
        document.getElementById('statusDisplay').innerHTML = `<span class="bg-yellow-200 text-yellow-800 px-2 py-1 rounded">${document.getElementById('statusInput').value}</span>`;
        document.getElementById('sourceDisplay').innerText = document.getElementById('sourceInput').value;
        document.getElementById('opportunityAmountDisplay').innerText = document.getElementById('opportunityAmountInput').value;
        document.getElementById('campaignDisplay').innerText = document.getElementById('campaignInput').value;
        document.getElementById('industryDisplay').innerText = document.getElementById('industryInput').value;
        document.getElementById('descriptionDisplay').innerText = document.getElementById('descriptionInput').value;

        editButton.innerText = 'Edit'; // Change button text back to Edit
    } else {
        editButton.innerText = 'Save'; // Change button text to Save
    }
    document.getElementById('editMode').value = !isEditing; // Toggle edit mode
}
function toggleFollowed() {
    const wifiIcon = document.getElementById('wifiIcon');
    const followButton = document.getElementById('followButton');

    // Toggle the display of the Wi-Fi icon
    if (wifiIcon.style.display === 'none') {
        wifiIcon.style.display = 'inline'; // Show the icon
        followButton.innerText = 'follow';
    } else {
        wifiIcon.style.display = 'none'; // Hide the icon
    }
}