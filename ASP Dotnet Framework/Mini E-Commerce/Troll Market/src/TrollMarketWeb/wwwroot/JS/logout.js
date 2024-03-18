(() =>
{
    let logoutButton = document.querySelector('button');
    logoutButton.addEventListener('click',function(event)
    {
        localStorage.removeItem("token");
        localStorage.removeItem("username");
    });
})();