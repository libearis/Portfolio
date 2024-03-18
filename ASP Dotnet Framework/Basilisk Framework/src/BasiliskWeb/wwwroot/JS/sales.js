(() =>
{
    initiateButtonFunction();
    getSalesAPI();
    function initiateButtonFunction()
    {
        let insertNewButton = document.querySelector('.create-button');
        let closeInsertFormButton = document.querySelector('.sales-form-flex button');
        let closeEditFormButton = document.querySelector('.sales-edit-flex button');
        let closeSalesDetailButton = document.querySelector('.sales-detail-flex button');
        let submitInsertNew = document.querySelector('form button#insertsubmit');
        let searchButton = document.querySelector('#searchbutton');

        searchButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let cityNameInput = document.querySelector('#nameinputsearch');
            let pageCount = document.querySelector('#pagecount');
            pageCount.textContent = 'default';

            let allTr = document.querySelectorAll('.maintbody tr');
            for(let tr of allTr)
            {
                tr.remove();
            }
            
            let allPaginationButton = document.querySelectorAll('#paginationdiv a');
            for(let button of allPaginationButton)
            {
                button.remove();
            }
            getSalesAPI(1, cityNameInput.value);
        });

        insertNewButton.addEventListener('click', function(event)
        {
            let insertForm = document.querySelector('.sales-form');
            insertForm.setAttribute('class', 'sales-form-active');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')
        });

        closeInsertFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.sales-form-active');
            insertForm.setAttribute('class', 'sales-form');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')
        });

        closeEditFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.sales-edit-active');
            insertForm.setAttribute('class', 'sales-edit');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')
        });

        closeSalesDetailButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.sales-detail-active');
            insertForm.classList.remove('class', 'sales-detail-active');
            insertForm.classList.add('class', 'sales-detail');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')

            let allTr = document.querySelectorAll('.salesdetailtbody tr');
            for(let tr of allTr)
            {
                tr.remove();
            }

            let allPaginationButton = document.querySelectorAll('#paginationdiv-detail a');
            for(let button of allPaginationButton)
            {
                button.remove();
            }
        });

        submitInsertNew.addEventListener('click', function(event)
        {
            postInsertAPI();
        });

    }

    function bindingIndex(salesJSON)
    {
        let indexSalesDTO = JSON.parse(salesJSON);
        let salesDTO = JSON.parse(salesJSON).salesDTOs;
        let tbody = document.querySelector('.maintbody');
        let pageCount = document.querySelector('#pagecount');
        if(pageCount.textContent == 'default')
        {
            pageCount.textContent = `Page 1 of ${indexSalesDTO.pagination.totalPages}`
        }

        for(let index = 0; index < salesDTO.length; index++)
        {
            let tr = document.createElement('tr');
            tbody.append(tr);

            let tdButton = document.createElement('td');
            let buttonEdit = document.createElement('button');
            let buttonDelete = document.createElement('button');
            let buttonRegion = document.createElement('button');
            let deleteSpan = document.createElement('span');
            let salesSpan = document.createElement('span');

            buttonEdit.setAttribute('class', 'blue-button')
            buttonEdit.textContent = 'Edit'

            deleteSpan.textContent = ' ';
            buttonDelete.setAttribute('class', 'blue-button');
            buttonDelete.textContent = 'Delete';

            salesSpan.textContent = ' ';
            buttonRegion.setAttribute('class', 'blue-button');
            buttonRegion.textContent = 'region';

            let tdEmployeeNumber = document.createElement('td');
            tdEmployeeNumber.textContent = salesDTO[index].employeeNumber;
            let tdFullName = document.createElement('td');
            tdFullName.textContent = salesDTO[index].fullName;
            let tdLevel = document.createElement('td');
            tdLevel.textContent = salesDTO[index].level;
            let tdSuperiorEmployee = document.createElement('td');
            tdSuperiorEmployee.textContent = salesDTO[index].superiorEmployeeName;

            tr.append(tdButton);
            tr.append(tdEmployeeNumber);
            tr.append(tdFullName);
            tr.append(tdLevel);
            tr.append(tdSuperiorEmployee);
            deleteSpan.append(buttonDelete);
            salesSpan.append(buttonRegion);
            tdButton.append(buttonEdit);
            tdButton.append(deleteSpan);
            tdButton.append(salesSpan);

            buttonEdit.addEventListener('click', function(event)
            {
                let editForm = document.querySelector('.sales-edit');
                editForm.setAttribute('class', 'sales-edit-active');

                let divForDisable = document.querySelector('.disable-container');
                divForDisable.classList.toggle('backgrounddisable')

                let nameInput = document.querySelector('#editname');
                let remarkInput = document.querySelector('#editremark');

                nameInput.value = salesDTO[index].city;
                remarkInput.value = salesDTO[index].remark;

                let submitEdit = document.querySelector('form button#editsubmit');
                submitEdit.addEventListener('click', function(event)
                {
                    postUpdateAPI(salesDTO[index].id);
                });
            });

            buttonDelete.addEventListener('click', function(event)
            {
                getDeleteAPI(salesDTO[index].id);
            });

            buttonRegion.addEventListener('click', function(event)
            {
                getSalesDetail(salesDTO[index].id);
                let detailForm = document.querySelector('.sales-detail');
                detailForm.classList.remove('class', '.sales-detail')
                detailForm.classList.add('class', 'sales-detail-active');

                let divForDisable = document.querySelector('.disable-container');
                divForDisable.classList.toggle('backgrounddisable')

                let pageCount = document.querySelector('#pagecount-detail');
                pageCount.textContent = 'default'
            });
        }

        for(let index = 1; index <= indexSalesDTO.pagination.totalPages; index++)
        {
            let paginationDiv = document.querySelector('#paginationdiv');
            let buttonForPage = document.createElement('a');
            buttonForPage.setAttribute('class', 'number');
            buttonForPage.textContent = index;

            paginationDiv.append(buttonForPage);

            buttonForPage.addEventListener('click', function(event)
            {
                let allPaginationButton = document.querySelectorAll('#paginationdiv a');
                pageCount.textContent = `Page ${index} of ${indexSalesDTO.pagination.totalPages}`;
                let allTr = document.querySelectorAll('.maintbody tr');
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getSalesAPI(index);
            });
        }
    }

    function bindingSalesDetail()
    {

    }

    function getSalesAPI(pageNumber = 1)
    {
        const url = `http://localhost:8000/api/v1/sales?pageNumber=${pageNumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingIndex(request.response);
        }
    }
})();