(() =>
{
    buttonAddEvent();
    function buttonAddEvent()
    {
        let submitButton = document.querySelector('#submit');

        submitButton.addEventListener('click', function(event)
        {
            loginPost();
        });
    }

    function setToken(json)
    {
        let tokenJson = JSON.parse(json);
        localStorage.setItem("token" , `Bearer ${tokenJson.token}`);
    }

    function loginForm()
    {
        let usernameValue = document.querySelector('#insert1').value;
        let passwordValue = document.querySelector('#insert2').value;
        localStorage.setItem('username', usernameValue);
        return {
            "username": usernameValue,
            "password": passwordValue
        };
    }

    function loginPost()
    {
        const url = `http://localhost:8081/api/v1/Auth`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(loginForm()));
        request.onload = function()
        {
            setToken(request.response);
        }
    }
})();