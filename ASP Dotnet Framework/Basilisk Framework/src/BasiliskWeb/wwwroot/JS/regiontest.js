(() =>
{
    initiateButtonFunction();
    function initiateButtonFunction()
    {
        let insertNewButton = document.querySelector('.create-button');
        let closeInsertFormButton = document.querySelector('.region-form-flex button');
        let closeEditFormButton = document.querySelector('.region-edit-flex button');
        let closeRegionDetailButton = document.querySelector('.region-detail-flex button');
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
            getRegionAPI(1, cityNameInput.value);
        });

        insertNewButton.addEventListener('click', function(event)
        {
            let insertForm = document.querySelector('.region-form');
            insertForm.setAttribute('class', 'region-form-active');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')
        });

        closeInsertFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.region-form-active');
            insertForm.setAttribute('class', 'region-form');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')
        });

        closeEditFormButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.region-edit-active');
            insertForm.setAttribute('class', 'region-edit');

            let divForDisable = document.querySelector('.disable-container');
            divForDisable.classList.toggle('backgrounddisable')
        });

        closeRegionDetailButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.region-detail-active');
            insertForm.classList.remove('class', 'region-detail-active');
            insertForm.classList.add('class', 'region-detail');

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

    function BindingIndex(regionJSON)
    {
        let indexRegionDTO = JSON.parse(regionJSON);
        let regionDTO = JSON.parse(regionJSON).regionDTOs;
        let tbody = document.querySelector('.maintbody');
        let pageCount = document.querySelector('#pagecount');
        if(pageCount.textContent == 'default')
        {
            pageCount.textContent = `Page 1 of ${indexRegionDTO.pagination.totalPages}`
        }

        for(let index = 0; index < regionDTO.length; index++)
        {
            let tr = document.createElement('tr');
            tbody.append(tr);

            let tdButton = document.createElement('td');
            let buttonEdit = document.createElement('button');
            let buttonDelete = document.createElement('button');
            let buttonSalesmen = document.createElement('button');
            let deleteSpan = document.createElement('span');
            let salesSpan = document.createElement('span');

            buttonEdit.setAttribute('class', 'blue-button')
            buttonEdit.textContent = 'Edit'

            deleteSpan.textContent = ' ';
            buttonDelete.setAttribute('class', 'blue-button');
            buttonDelete.textContent = 'Delete';

            salesSpan.textContent = ' ';
            buttonSalesmen.setAttribute('class', 'blue-button');
            buttonSalesmen.textContent = 'Salesmen';

            let tdCityName = document.createElement('td');
            tdCityName.textContent = regionDTO[index].city;
            let tdRemark = document.createElement('td');
            tdRemark.textContent = regionDTO[index].remark;

            tr.append(tdButton);
            tr.append(tdCityName);
            tr.append(tdRemark);
            deleteSpan.append(buttonDelete);
            salesSpan.append(buttonSalesmen);
            tdButton.append(buttonEdit);
            tdButton.append(deleteSpan);
            tdButton.append(salesSpan);

            buttonEdit.addEventListener('click', function(event)
            {
                let editForm = document.querySelector('.region-edit');
                editForm.setAttribute('class', 'region-edit-active');

                let divForDisable = document.querySelector('.disable-container');
                divForDisable.classList.toggle('backgrounddisable')

                let nameInput = document.querySelector('#editname');
                let remarkInput = document.querySelector('#editremark');

                nameInput.value = regionDTO[index].city;
                remarkInput.value = regionDTO[index].remark;

                let submitEdit = document.querySelector('form button#editsubmit');
                submitEdit.addEventListener('click', function(event)
                {
                    postUpdateAPI(regionDTO[index].id);
                });
            });

            buttonDelete.addEventListener('click', function(event)
            {
                getDeleteAPI(regionDTO[index].id);
            });

            buttonSalesmen.addEventListener('click', function(event)
            {
                getSalesDetail(regionDTO[index].id);
                let detailForm = document.querySelector('.region-detail');
                detailForm.classList.remove('class', '.region-detail')
                detailForm.classList.add('class', 'region-detail-active');

                let divForDisable = document.querySelector('.disable-container');
                divForDisable.classList.toggle('backgrounddisable')

                let pageCount = document.querySelector('#pagecount-detail');
                pageCount.textContent = 'default'
            });
        }

        for(let index = 1; index <= indexRegionDTO.pagination.totalPages; index++)
        {
            let paginationDiv = document.querySelector('#paginationdiv');
            let buttonForPage = document.createElement('a');
            buttonForPage.setAttribute('class', 'number');
            buttonForPage.textContent = index;

            paginationDiv.append(buttonForPage);

            buttonForPage.addEventListener('click', function(event)
            {
                let cityNameInput = document.querySelector('#nameinputsearch');
                let allPaginationButton = document.querySelectorAll('#paginationdiv a');
                pageCount.textContent = `Page ${index} of ${indexRegionDTO.pagination.totalPages}`;
                let allTr = document.querySelectorAll('.maintbody tr');
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getRegionAPI(index, cityNameInput.value);
            });
        }
    }

    function bindingSalesDetail(salesJSON)
    {
        let indexSalesDetailDTO = JSON.parse(salesJSON); 
        let salesDetailDTO = JSON.parse(salesJSON).salesDetailDTOs;
        let tbody = document.querySelector('.salesdetailtbody');

        let cityDetail = document.querySelector('.cityname-detail');
        let remarkDetail = document.querySelector('.remark-detail');
        
        cityDetail.textContent = indexSalesDetailDTO.city;
        remarkDetail.textContent = indexSalesDetailDTO.remark;

        for(let index = 0; index < salesDetailDTO.length; index++)
        {
            let tr = document.createElement('tr');
            tbody.append(tr);

            let tdEmployeeNumber = document.createElement('td');
            tdEmployeeNumber.textContent = salesDetailDTO[index].employeeNumber
            let tdFullName = document.createElement('td');
            tdFullName.textContent = salesDetailDTO[index].fullName
            let tdLevel = document.createElement('td');
            tdLevel.textContent = salesDetailDTO[index].level
            let tdSuperior = document.createElement('td');
            tdSuperior.textContent = salesDetailDTO[index].superiorEmployeeName;

            tr.append(tdEmployeeNumber);
            tr.append(tdFullName);
            tr.append(tdLevel);
            tr.append(tdSuperior);
        }

        let pageCount = document.querySelector('#pagecount-detail');
        if(pageCount.textContent == 'default')
        {
            pageCount.textContent = `Page 1 of ${indexSalesDetailDTO.pagination.totalPages}`
        }

        for(let index = 1; index <= indexSalesDetailDTO.pagination.totalPages; index++)
        {
            let paginationDetailDiv = document.querySelector('#paginationdiv-detail');
            let buttonForPageDetail = document.createElement('a');

            buttonForPageDetail.setAttribute('class', 'number');
            buttonForPageDetail.textContent = index;

            paginationDetailDiv.append(buttonForPageDetail);

            buttonForPageDetail.addEventListener('click', function(event)
            {
                let allTr = document.querySelectorAll('.salesdetailtbody tr');
                let allPaginationButton = paginationDetailDiv.querySelectorAll('a');
                pageCount.textContent = `Page ${index} of ${indexSalesDetailDTO.pagination.totalPages}`;
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getSalesDetail(indexSalesDetailDTO.regionId, index);
            });
        }
    }

    function getRegionAPI(pagenumber = 1, cityName = "")
    {
        const url = `http://localhost:8000/api/v1/regions?pageNumber=${pagenumber}&pageSize=5&cityName=${cityName}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            BindingIndex(request.response);
        }
    }

    function insertNewRegion()
    {
        let formInsert = document.querySelector('.region-form-active');
        const newRegionName = formInsert.querySelector('#nameinput').value;
        const newRemark = formInsert.querySelector('#remarkinput').value;
        return {'city': newRegionName,
                'remark' : newRemark}
    }

    function editRegion(id)
    {
        let formInsert = document.querySelector('.region-edit-active');
        const newRegionName = formInsert.querySelector('#editname').value;
        const newRemark = formInsert.querySelector('#editremark').value;
        return {'id' : id,
                'city': newRegionName, 
                'remark' : newRemark}
    }

    function postInsertAPI()
    {
        const url = 'http://localhost:8000/api/v1/regions';
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertNewRegion()));
        request.onload = function()
        {
            console.log(request.response);
        }
    }

    function postUpdateAPI(id)
    {
        const url = `http://localhost:8000/api/v1/regions`;
        let request = new XMLHttpRequest();
        request.open('PUT', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(editRegion(id)));
        request.onload = function()
        {
            console.log(request.response);
        }
    }

    function getDeleteAPI(id)
    {
        const url = `http://localhost:8000/api/v1/regions/${id}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            location.reload();
        }
    }

    function getSalesDetail(id, pageNumber = 1)
    {
        const url = `http://localhost:8000/api/v1/regions/sales-detail?id=${id}&pageNumber=${pageNumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingSalesDetail(request.response);
        }
    }

    getRegionAPI();
})();