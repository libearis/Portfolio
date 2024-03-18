(() =>{
    getAPI();

    function bindingButton(json)
    {
        let customersJson = JSON.parse(json);
        let viewMemberButton = document.querySelectorAll('#view-member');
        let popUpForm = document.querySelector('.insert-new');
        let closePopup = document.querySelector('#close-form');
        closePopup.addEventListener('click', function(event)
        {
            popUpForm.setAttribute('class', 'insert-new')
        });

        for(let index = 0; index < customersJson.length; index++)
        {
            viewMemberButton[index].addEventListener('click', function(event)
            {
                event.preventDefault();
                popUpForm.setAttribute('class', 'insert-new-active')

                let tdMemberNumber = document.querySelector('#info1');
                let tdFullName = document.querySelector('#info2');
                let tdBirthDate = document.querySelector('#info3');
                let tdGender = document.querySelector('#info4');

                tdMemberNumber.textContent = customersJson[index].membershipNumber;
                tdFullName.textContent = customersJson[index].fullName;
                tdBirthDate.textContent = customersJson[index].birthDate;
                tdGender.textContent = customersJson[index].gender;
            });
        }
    }

    function getAPI()
    {
        const url = `http://localhost:8000/api/v1/customer`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingButton(request.response);
        }
    }

    function getMemberByNumberAPI(memberNumber)
    {
        const url = `http://localhost:8000/api/v1/customer/${memberNumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            fillPopupData(request.response);
        }
    }
})();