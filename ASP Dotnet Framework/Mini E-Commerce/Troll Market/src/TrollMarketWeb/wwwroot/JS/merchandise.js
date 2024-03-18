(() => 
{
    if(sessionStorage.getItem('pagenumber') == null)
    {
        getAllSellerProduct(localStorage.getItem('username'), 1);
    }

    else
    {
        getAllSellerProduct(localStorage.getItem('username'), sessionStorage.getItem('pagenumber'));
    }

    function bindingSellerProduct(json)
    {
        let products = JSON.parse(json);
        let detailButton = document.querySelectorAll('#detailbutton');

        let closeDetailButton = document.querySelector('#close-detail');
        closeDetailButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let detailForm = document.querySelector('.pop-up-detail');
            detailForm.classList.toggle('pop-up-detail-active');
        });
        
        for(let index = 0; index < products.shopDTOs.length; index++)
        {
            detailButton[index].addEventListener('click', function(event)
            {
                event.preventDefault();
                let detailForm = document.querySelector('.pop-up-detail');
                detailForm.classList.toggle('pop-up-detail-active');

                document.querySelector('#info1').textContent = products.shopDTOs[index].name
                document.querySelector('#info2').textContent = products.shopDTOs[index].categoryName
                document.querySelector('#info3').textContent = products.shopDTOs[index].description
                document.querySelector('#info4').textContent = products.shopDTOs[index].unitPrice
                document.querySelector('#info5').textContent = products.shopDTOs[index].discontinue
            });
        }

        let pageNumberButton = document.querySelectorAll('.pagenumber');
        for(let index = 1; index <= products.pagination.totalPages; index++)
        {
            pageNumberButton[index - 1].addEventListener('click', function(event)
            {
                sessionStorage.setItem('pagenumber', index);
            });
        }
    }

    function getAllSellerProduct(username, pageNumber = 1)
    {
        const url = `http://localhost:8081/api/v1/merch?username=${username}&pageNumber=${pageNumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.send();
        request.onload = function()
        {
            bindingSellerProduct(request.response);
        }
    }
})();