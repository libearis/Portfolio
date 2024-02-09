(() =>
{
    localStorage.clear()
    getToDosAPI();
    function getUsersAPI(todos)
    {
        const url = 'https://jsonplaceholder.typicode.com/users';
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingUsers(request.response, todos);
        }
    }
    function getToDosAPI()
    {
        const url = 'https://jsonplaceholder.typicode.com/todos';
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            getUsersAPI(request.response)
        }
    }
    let bindingUsers = (json, todos) =>
    {
        let usersObject = JSON.parse(json);
        let todosObject = JSON.parse(todos);
        let tbody = document.querySelector('tbody');
        for(let index = 0; index < usersObject.length; index++)
        {
            let tr = document.createElement('tr');
            tbody.append(tr);

            let tdButton = document.createElement('td');
            let buttonContact = document.createElement('button');
            let buttonPost = document.createElement('button');
            let buttonToDo = document.createElement('button');
            let tdName = document.createElement('td');
            let tdUsername = document.createElement('td');
            let tdEmail = document.createElement('td');
            let tdCompany = document.createElement('td');
            
            let trSelector = document.querySelector(`tbody tr:nth-child(${index + 1})`);

            trSelector.append(tdButton);
            tdButton.append(buttonContact);
            tdButton.append(buttonPost);
            tdButton.append(buttonToDo);
            trSelector.append(tdName);
            trSelector.append(tdUsername);
            trSelector.append(tdEmail);
            trSelector.append(tdCompany);

            buttonContact.textContent = 'Contact';
            buttonPost.textContent = 'Posts';
            buttonToDo.textContent = 'To-Do';
            tdName.textContent = usersObject[index].name;
            tdUsername.textContent = usersObject[index].username;
            tdEmail.textContent = usersObject[index].email;
            tdCompany.textContent = usersObject[index].company.name;

            buttonContact.addEventListener('click', function(event)
            {
                let allSpan = document.querySelectorAll('span')
                for(let span of allSpan)
                {
                    span.remove()
                }
                let popUp = document.querySelector('.popup div:first-child');
                popUp.classList.toggle('popupactive');

                let street = document.querySelector('.contactaddress ul li:nth-child(1)');
                let spanStreet = document.createElement('span');
                spanStreet.textContent = usersObject[index].address.street;
                street.append(spanStreet);

                let suite = document.querySelector('.contactaddress ul li:nth-child(2)');
                let spanSuite = document.createElement('span');
                spanSuite.textContent = usersObject[index].address.suite;
                suite.append(spanSuite);

                let city = document.querySelector('.contactaddress ul li:nth-child(3)');
                let spanCity = document.createElement('span');
                spanCity.textContent = usersObject[index].address.city;
                city.append(spanCity);

                let zipCode = document.querySelector('.contactaddress ul li:nth-child(4)');
                let spanZipCode = document.createElement('span');
                spanZipCode.textContent = usersObject[index].address.zipcode;
                zipCode.append(spanZipCode);

                let phone = document.querySelector('.contactphone');
                let spanPhone = document.createElement('span');
                spanPhone.textContent = usersObject[index].phone
                phone.append(spanPhone)

                let website = document.querySelector('.contactwebsite');
                let spanWebsite = document.createElement('span');
                spanWebsite.textContent = usersObject[index].website
                website.append(spanWebsite)

                let closeButton = document.querySelector('#closebutton');
                closeButton.addEventListener('click', function(event)
                {
                    popUp.removeAttribute('class', 'popupactive')
                });
            })
            buttonToDo.addEventListener('click', function(event)
            {
                let toDoContainer = document.querySelector('.todo');
                let divsToDo = toDoContainer.querySelectorAll('div');
                for(div of divsToDo)
                {
                    div.remove()
                }
                for(todo of todosObject)
                {
                    if(todo.userId == usersObject[index].id)
                    {
                        let divToDo = document.createElement('div');
                        if(todo.completed == true)
                        {
                            divToDo.textContent = todo.title + ' (completed)';
                        }
                        else
                        {
                            divToDo.textContent = todo.title
                        }
                        toDoContainer.append(divToDo);
                    }
                }
            });
            buttonPost.addEventListener('click', function(event)
            {
                location.href = '../HTML/userpost.html'
                localStorage.setItem('userId', `${usersObject[index].id}`)
                localStorage.setItem('userName', `${usersObject[index].name}`)
            })
        }
    };
})()