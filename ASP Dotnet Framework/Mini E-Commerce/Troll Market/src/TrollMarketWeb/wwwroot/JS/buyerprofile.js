(() =>
{
    buttonAddEvent();
    function buttonAddEvent()
    {
        let validationError = document.querySelectorAll('.field-validation-error');
        for(let error of validationError)
        {
            error.style.backgroundColor = "#81ecec";
            error.style.color = "red";
        }

        let topupButton = document.querySelector('#topup');
        let closeFormButton = document.querySelector('#close-form');
        topupButton.addEventListener('click', function(event)
        {
            let topupForm = document.querySelector('.topup');
            topupForm.classList.toggle('topup-active');
        });

        closeFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let topupForm = document.querySelector('.topup');
            topupForm.classList.toggle('topup-active');
        });
    }
})();