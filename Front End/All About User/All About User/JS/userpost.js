(() =>
{
    getUserPosts()
    function getUserPosts()
    {
        const url = 'https://jsonplaceholder.typicode.com/posts';
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            userPosts(request.response);
        }
    }
    function userPosts(json)
    {
        let postObject = JSON.parse(json)
        let tbody = document.querySelector('tbody')
        let h1 = document.querySelector('h1')
        let backButton = document.querySelector('.backtouser')
        
        backButton.addEventListener('click', function(event)
        {
            location.href = '../HTML/index.html'
        })
        h1.textContent = `${localStorage.getItem('userName')}'s Posts`
        
        for(let index = 0; index < postObject.length; index++)
        {
            if(postObject[index].userId == localStorage.getItem('userId'))
            {
                let tr = document.createElement('tr')
                let tdTittle = document.createElement('td')
                let tdBody = document.createElement('td')
                
                tdTittle.textContent = postObject[index].title
                tdBody.textContent = postObject[index].body
                
                tr.append(tdTittle)
                tr.append(tdBody)
                tbody.append(tr)
            }
        }
    }
})()