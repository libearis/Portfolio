(() =>
{
    if(sessionStorage.getItem('pagenumber') == null)
    {
        getAllProduct(1, document.querySelector('#search1').value, 
        document.querySelector('#search2').value, document.querySelector('#search3').value);
    }

    else
    {
        getAllProduct(sessionStorage.getItem('pagenumber'), document.querySelector('#search1').value, 
        document.querySelector('#search2').value, document.querySelector('#search3').value);
    }
    buttonAddEvent();
    function buttonAddEvent()
    {
        let closeFormButton = document.querySelector('#close-form');
        let closeDetailButton = document.querySelector('#close-detail');
        let searchButton = document.querySelector('#search');

        closeFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let topupForm = document.querySelector('.topup');
            topupForm.classList.toggle('topup-active');
        });

        closeDetailButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let detailForm = document.querySelector('.pop-up-detail');
            detailForm.classList.toggle('pop-up-detail-active');
        });

        searchButton.addEventListener('click', function(event)
        {
            sessionStorage.removeItem('pagenumber');
        });
    }

    function bindingProduct(json)
    {
        let products = JSON.parse(json);
        let addToCartButton = document.querySelectorAll('#addtocart');
        let detailButton = document.querySelectorAll('#detailbutton');
        for(let index = 0; index < products.shopDTOs.length; index++)
        {
            addToCartButton[index].addEventListener('click', function(event)
            {
                event.preventDefault();
                let topupForm = document.querySelector('.topup');
                topupForm.classList.toggle('topup-active');

                let submitNewButton = document.querySelector('#submit-new');
                submitNewButton.addEventListener('click', function(event)
                {
                    event.preventDefault();
                    if(document.querySelector('#insert1').value == '' 
                    || document.querySelector('#insert2').value == '')
                    {
                        document.querySelector('#insert1').value == ''?
                        document.querySelector('#errorquantity').textContent = 'Wajib diisi':
                        document.querySelector('#errorquantity').textContent = ''

                        document.querySelector('#insert2').value == ''?
                        document.querySelector('#errorshipment').textContent = 'Wajib diisi':
                        document.querySelector('#errorshipment').textContent = ''
                    }
                    else
                    {
                        addToCart(products.shopDTOs[index].id);
                    }
                });
            });

            detailButton[index].addEventListener('click', function(event)
            {
                event.preventDefault();
                let detailForm = document.querySelector('.pop-up-detail');
                detailForm.classList.toggle('pop-up-detail-active');

                document.querySelector('#info1').textContent = products.shopDTOs[index].name
                document.querySelector('#info2').textContent = products.shopDTOs[index].categoryName
                document.querySelector('#info3').textContent = products.shopDTOs[index].description
                document.querySelector('#info4').textContent = products.shopDTOs[index].unitPrice
                document.querySelector('#info5').textContent = products.shopDTOs[index].sellerName
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

    function addToCartForm(id)
    {
        let quantityValue = document.querySelector('#insert1').value;
        let shipmentValue = document.querySelector('#insert2').value;

        return {
            "username" : localStorage.getItem('username'),
            "productId": id,
            "shipmentId": shipmentValue,
            "quantity": quantityValue
        };
    }

    function getAllProduct(pageNumber = 1, productName ="", categoryName ="", description ="")
    {
        const url = `http://localhost:8081/api/v1/shop?pageNumber=${pageNumber}&productName=${productName}&categoryName=${categoryName}&description=${description}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.send();
        request.onload = function()
        {
            bindingProduct(request.response);
        }
    }

    function addToCart(id)
    {
        const url = `http://localhost:8081/api/v1/shop/addToCart`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(addToCartForm(id)));
        request.onload = function()
        {
            location.reload();
        }
    }
})();