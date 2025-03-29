
document.querySelector('.convert').addEventListener('click', function () {
    const accountChecked = document.getElementById('account').checked;
    const contactChecked = document.getElementById('contact').checked;
    const opportunityChecked = document.getElementById('opportunity').checked;

    if (accountChecked || contactChecked || opportunityChecked) {
        alert('Converting selected options...');
        // Add your conversion logic here
    } else {
        alert('Please select at least one option to convert.');
    }
});

document.querySelector('.cancel').addEventListener('click', function () {
    // Logic to handle cancellation
    alert('Conversion cancelled.');
});