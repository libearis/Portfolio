(() =>{
    getCategoryAPI();
    buttonFunction();

    function buttonFunction()
    {
        let addNewButton = document.querySelector('.add-category');
        let closeFormButton = document.querySelector('#close-form');
        let searchButton = document.querySelector('#search-button');
        
        addNewButton.addEventListener('click', function(event){
            let newCategory = document.querySelector('#insert-1');
            newCategory.removeAttribute('disabled');
            let newFloor = document.querySelector('#insert-2');
            newFloor.removeAttribute('disabled');
            let newIsle = document.querySelector('#insert-3');
            newIsle.removeAttribute('disabled');
            let newBay = document.querySelector('#insert-4');
            newBay.removeAttribute('disabled');

            newCategory.removeAttribute('disabled');
            newCategory.value = '';
            newFloor.value = '';
            newIsle.value = '';
            newBay.value = '';
            
            let popupForm = document.querySelector('.insert-new');
            popupForm.setAttribute('class', 'insert-new-active');

            let h2Text = document.querySelector('.header-flex h2');
            h2Text.textContent = 'Insert New Category';

            let submitNewCategory = document.querySelector('#submit-insertcategory');
            submitNewCategory.textContent = 'Insert New Category';
            submitNewCategory.addEventListener('click', function(event)
            {
                event.preventDefault();
                if(newCategory.value == '' || newFloor.value == '' || newIsle.value == '' || newBay.value == '')
                {
                    alert("All field cannot be empty");
                }
                else
                {
                    insertCategoryAPI();
                }
            });
        });

        closeFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let popupForm = document.querySelector('.insert-new-active');
            popupForm.setAttribute('class', 'insert-new')
        });

        searchButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let search1 = document.querySelector('#search1');
            let allTr = document.querySelectorAll('.main-tbody tr');
            let allPaginationButton = document.querySelectorAll('.tfoot-flex div+div button');
            
            let pageCountDiv = document.querySelector('.tfoot-flex div');
            pageCountDiv.textContent = 'default';
            
            for(let tr of allTr)
            {
                tr.remove();
            }
            for(let button of allPaginationButton)
            {
                button.remove();
            }
            getCategoryAPI(1, search1.value);
        });
    }
    function bindingIndex(categoryJSON)
    {
        let pagination = JSON.parse(categoryJSON).pagination
        let categoryDTO = JSON.parse(categoryJSON).categories;
        let mainTbody = document.querySelector('.main-tbody');
        for(let index = 0; index < categoryDTO.length; index++)
        {
            let tr = document.createElement('tr');
            mainTbody.append(tr);

            let tdButton = document.createElement('td');
            let buttonEdit = document.createElement('button');
            let buttonDelete = document.createElement('button');
            let buttonBooks = document.createElement('button');

            buttonEdit.textContent = 'Edit';
            buttonEdit.setAttribute('class', 'button-25')
            buttonDelete.textContent = 'Delete';
            buttonDelete.setAttribute('class', 'button-25')
            buttonBooks.textContent = 'Books'
            buttonBooks.setAttribute('class', 'button-25')

            let tdCategoryName = document.createElement('td');
            tdCategoryName.textContent = categoryDTO[index].name;
            let tdFloor = document.createElement('td');
            tdFloor.textContent = categoryDTO[index].floor;
            let tdIsle = document.createElement('td');
            tdIsle.textContent = categoryDTO[index].isle;
            let tdBay = document.createElement('td');
            tdBay.textContent = categoryDTO[index].bay;
            let tdTotalBook = document.createElement('td');
            tdTotalBook.textContent = categoryDTO[index].totalBook;

            tr.append(tdButton);
            tr.append(tdCategoryName);
            tr.append(tdFloor);
            tr.append(tdIsle);
            tr.append(tdBay);
            tr.append(tdTotalBook);
            tdButton.append(buttonEdit);
            tdButton.append(buttonDelete);
            tdButton.append(buttonBooks);

            buttonBooks.addEventListener('click', function(event){
                location.href = `Book/${categoryDTO[index].name}`;
                localStorage.setItem("categoryName", categoryDTO[index].name);
            });

            buttonEdit.addEventListener('click', function(event)
            {
                let h2Text = document.querySelector('.header-flex h2');
                h2Text.textContent = `Update ${categoryDTO[index].name}`;
                let popupForm = document.querySelector('.insert-new');
                popupForm.setAttribute('class', 'insert-new-active');

                let updateCategory = document.querySelector('#insert-1');
                let updateFloor = document.querySelector('#insert-2');
                let updateIsle = document.querySelector('#insert-3');
                let updateBay = document.querySelector('#insert-4');

                updateCategory.value = categoryDTO[index].name;
                updateCategory.setAttribute('disabled', 'true');
                updateFloor.value = categoryDTO[index].floor;
                updateFloor.removeAttribute('disabled');
                updateIsle.value = categoryDTO[index].isle;
                updateIsle.removeAttribute('disabled');
                updateBay.value = categoryDTO[index].bay;
                updateBay.removeAttribute('disabled');
                
                let submitUpdateCategory = document.querySelector('#submit-insertcategory');
                submitUpdateCategory.textContent = `Update ${categoryDTO[index].name}`;
                submitUpdateCategory.addEventListener('click', function(event)
                {
                    event.preventDefault();
                    editCategoryAPI(categoryDTO[index].name);
                });
            });

            buttonDelete.addEventListener('click', function(event)
            {
                event.preventDefault();
                let popupForm = document.querySelector('.insert-new');
                popupForm.setAttribute('class', 'insert-new-active');

                let h2Text = document.querySelector('.header-flex h2');
                h2Text.textContent = 'Delete this Category?';

                let updateCategory = document.querySelector('#insert-1');
                let updateFloor = document.querySelector('#insert-2');
                let updateIsle = document.querySelector('#insert-3');
                let updateBay = document.querySelector('#insert-4');

                updateCategory.value = categoryDTO[index].name;
                updateCategory.setAttribute('disabled', 'true');
                updateFloor.value = categoryDTO[index].floor;
                updateFloor.setAttribute('disabled', 'false');
                updateIsle.value = categoryDTO[index].isle;
                updateIsle.setAttribute('disabled', 'false');
                updateBay.value = categoryDTO[index].bay;
                updateBay.setAttribute('disabled', 'false');

                let submitDeleteCategory = document.querySelector('#submit-insertcategory');
                submitDeleteCategory.textContent = `Delete ${categoryDTO[index].name}`;
                submitDeleteCategory.addEventListener('click', function(event)
                {
                    event.preventDefault();
                    if(categoryDTO[index].totalBook != 0)
                    {
                        alert("This category cannot be deleted because there is book dependant on it. Please delete the corresponding book first");
                    }
                    else
                    {
                        deleteCategoryAPI(categoryDTO[index].name);
                    }
                });
            });
        }

        let pageCountDiv = document.querySelector('.tfoot-flex div');
        if(pageCountDiv.textContent == 'default')
        {
            pageCountDiv.textContent = `Page 1 of ${pagination.totalPages}`
        }

        for(let index = 1; index <= pagination.totalPages; index++)
        {
            let listPageDiv = document.querySelector('.tfoot-flex div+div');
            let pageButton = document.createElement('button');
            pageButton.setAttribute('class', 'button-25');
            pageButton.textContent = index;

            listPageDiv.append(pageButton);

            pageButton.addEventListener('click', function(event)
            {
                let search1 = document.querySelector('#search1');
                let allTr = document.querySelectorAll('.main-tbody tr');
                let allPaginationButton = listPageDiv.querySelectorAll('button');
                pageCountDiv.textContent = `Page ${index} of ${pagination.totalPages}`;
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getCategoryAPI(index, search1.value);
            });
        }
    }

    function nameValidation(json, newCategoryName)
    {
        let nameJson = JSON.parse(json).categories;
        let isExist = false;
        for(let index = 0; index < nameJson.length; index++)
        {
            if(nameJson[index].name == newCategoryName)
            {
                isExist = true;
                break;
            }
        }
        if(isExist)
        {
            alert(`Category with name ${newCategoryName} is already exist, please use another name or edit the existed category`);
            location.reload();
        }
        else
        {
            insertCategoryAPI();
        }
    }

    function insertCategory()
    {
        const newCategory = document.querySelector('#insert-1').value;
        const newFloor = document.querySelector('#insert-2').value;
        const newIsle = document.querySelector('#insert-3').value;
        const newBay = document.querySelector('#insert-4').value;
        return {'name': newCategory,
                'floor' : newFloor,
                'isle' : newIsle,
                'bay' : newBay
            }
    }

    function editCategory(categoryName)
    {
        const newFloor = document.querySelector('#insert-2').value;
        const newIsle = document.querySelector('#insert-3').value;
        const newBay = document.querySelector('#insert-4').value;
        return {'name': categoryName,
                'floor' : newFloor,
                'isle' : newIsle,
                'bay' : newBay
            }
    }

    function getCategoryAPI(pagenumber = 1, cityName = "")
    {
        const url = `http://localhost:8000/api/v1/category?cityName=${cityName}&pageNumber=${pagenumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingIndex(request.response);
        }
    }

    function categoryNameValidation(newCategoryName, pagenumber = 1, cityName = "")
    {
        const url = `http://localhost:8000/api/v1/category?cityName=${cityName}&pageNumber=${pagenumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            nameValidation(request.response, newCategoryName)
        }
    }

    function insertCategoryAPI()
    {
        const url = `http://localhost:8000/api/v1/category/Insert`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertCategory()));
        request.onload = function()
        {
            location.reload();
        }
    }

    function editCategoryAPI(categoryName)
    {
        const url = `http://localhost:8000/api/v1/category/Update`;
        let request = new XMLHttpRequest();
        request.open('PUT', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(editCategory(categoryName)));
        request.onload = function()
        {
            location.reload();
        }
    }
    
    function deleteCategoryAPI(categoryName)
    {
        const url = `http://localhost:8000/api/v1/category/Delete-${categoryName}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            location.reload();
        }
    }
})();