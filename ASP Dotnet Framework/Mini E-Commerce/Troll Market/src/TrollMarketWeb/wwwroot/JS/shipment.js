(() =>
{
    getAllShipmentAPI();
    buttonAddEvent();
    function buttonAddEvent()
    {
        let addNewButton = document.querySelector('#firstbutton');
        addNewButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let detailForm = document.querySelector('.pop-up-detail');
            detailForm.classList.toggle('pop-up-detail-active');
            document.querySelector('#edit').style.display = 'none';
            document.querySelector('#tambah').style.display = 'inline';
            document.querySelector('#tambah').addEventListener('click', function(event)
            {
                if(document.querySelector('#insert1').value == '' || document.querySelector('#insert2').value == '')
                {
                    document.querySelector('#insert1').value == ''? 
                    document.querySelector('#error1').textContent = 'Nama Shipment tidak boleh kosong' : 
                    document.querySelector('#error1').textContent =''
                    document.querySelector('#insert2').value == ''? 
                    document.querySelector('#error2').textContent = 'Price tidak boleh kosong' : 
                    document.querySelector('#error2').textContent = ''
                }
                else
                {
                    alert('Berhasil ditambahkan');
                    addNewShipmentAPI();
                }
            });
        });
        
        let closeDetailButton = document.querySelector('#close-detail');
        let closeDeleteButton = document.querySelector('#close-delete');
        let closeServiceButton = document.querySelector('#close-service');

        closeDetailButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let detailForm = document.querySelector('.pop-up-detail');
            detailForm.classList.toggle('pop-up-detail-active');
        });
        
        closeDeleteButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let detailForm = document.querySelector('.pop-up-delete');
            detailForm.classList.toggle('pop-up-delete-active');
        });

        closeServiceButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let detailForm = document.querySelector('.pop-up-service');
            detailForm.classList.toggle('pop-up-service-active');
        });
    }

    function bindingShipment(json)
    {
        let shipments = JSON.parse(json);
        let tbody = document.querySelector('.maintbody');
        console.log(tbody);
        for(let index = 0; index < shipments.shipmentDTOs.length; index++)
        {
            let tr = document.createElement('tr');
            tbody.append(tr);

            let tdButton = document.createElement('td');
            if(shipments.shipmentDTOs[index].isUsed == false)
            {
                let buttonEdit = document.createElement('button');
                let buttonDelete = document.createElement('button');
    
                buttonEdit.textContent = 'Edit';
                buttonEdit.setAttribute('class', 'button-25');
                buttonDelete.textContent = 'Delete';
                buttonDelete.setAttribute('class', 'button-25');

                tdButton.append(buttonEdit);
                tdButton.append(buttonDelete);

                buttonDelete.addEventListener('click', function(event)
                {
                    let deleteConfirm = document.querySelector('.pop-up-delete');
                    deleteConfirm.classList.toggle('pop-up-delete-active');
                    document.querySelector('#delete-text h3').textContent = `Hapus Shipment ${shipments.shipmentDTOs[index].name}?`
                    document.querySelector('#delete').addEventListener('click', function(event)
                    {
                        alert(`${shipments.shipmentDTOs[index].name} berhasil dihapus`);
                        deleteShipmentAPI(shipments.shipmentDTOs[index].id);
                    });
                });

                buttonEdit.addEventListener('click', function(event)
                {
                    event.preventDefault();
                    let detailForm = document.querySelector('.pop-up-detail');
                    detailForm.classList.toggle('pop-up-detail-active');

                    document.querySelector('#insert1').value = shipments.shipmentDTOs[index].name;
                    document.querySelector('#insert2').value = shipments.shipmentDTOs[index].price;
                    document.querySelector('#insert3').checked = shipments.shipmentDTOs[index].service;
                    document.querySelector('#tambah').style.display = 'none';
                    document.querySelector('#edit').style.display = 'inline';
                    document.querySelector('#edit').addEventListener('click', function(event)
                    {
                        if(document.querySelector('#insert1').value == '' || document.querySelector('#insert2').value == '')
                        {
                            document.querySelector('#insert1').value == ''? 
                            document.querySelector('#error1').textContent = 'Nama Shipment tidak boleh kosong' : 
                            document.querySelector('#error1').textContent =''
                            document.querySelector('#insert2').value == ''? 
                            document.querySelector('#error2').textContent = 'Price tidak boleh kosong' : 
                            document.querySelector('#error2').textContent = ''
                        }
                        else
                        {
                            alert(`${shipments.shipmentDTOs[index].name} berhasil diedit`);
                            editShipmentAPI(shipments.shipmentDTOs[index].id)
                        }
                    });
                });
            }
            
            let tdShipmentName = document.createElement('td');
            tdShipmentName.textContent = shipments.shipmentDTOs[index].name;
            let tdPrice = document.createElement('td');
            tdPrice.textContent = shipments.shipmentDTOs[index].price;
            let tdService = document.createElement('td');
            tdService.textContent = shipments.shipmentDTOs[index].service == true? "Yes" : "No";

            tr.append(tdButton);
            tr.append(tdShipmentName);
            tr.append(tdPrice);
            tr.append(tdService);

            if(shipments.shipmentDTOs[index].service == true)
            {
                let buttonService = document.createElement('button');
                buttonService.textContent = 'Stop Service';
                buttonService.setAttribute('class', 'button-25');

                tdButton.append(buttonService);

                buttonService.addEventListener('click', function(event)
                {
                    let stopServiceConfirm = document.querySelector('.pop-up-service');
                    stopServiceConfirm.classList.toggle('pop-up-service-active');
                    document.querySelector('#stop-text h3').textContent = `Hentikan Service dari Shipment ${shipments.shipmentDTOs[index].name}?`
                    document.querySelector('#stopservice').addEventListener('click', function(event)
                    {
                        alert(`Service ${shipments.shipmentDTOs[index].name} telah diberhentikan`);
                        stopServiceAPI(shipments.shipmentDTOs[index].id);
                    });
                });
            }
        }

        let pageCountText = document.querySelector('.tfoot-flex div');
        if(pageCountText.textContent == 'default')
        {
            pageCountText.textContent = `Page 1 of ${shipments.pagination.totalPages}`
        }

        for(let index = 1; index <= shipments.pagination.totalPages; index++)
        {
            let listPageDiv = document.querySelector('.tfoot-flex div+div');
            let pageButton = document.createElement('button');
            pageButton.setAttribute('class', 'button-25');
            pageButton.textContent = index;

            listPageDiv.append(pageButton);

            pageButton.addEventListener('click', function(event)
            {
                event.preventDefault();
                let allTr = document.querySelectorAll('.maintbody tr');
                let allPaginationButton = listPageDiv.querySelectorAll('button');
                pageCountText.textContent = `Page ${index} of ${shipments.pagination.totalPages}`;
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getAllShipmentAPI(index);
            });
        }
    }

    function addNewForm()
    {
        let name = document.querySelector('#insert1').value;
        let price = document.querySelector('#insert2').value;
        let service = document.querySelector('#insert3').checked;

        return {
            "name" : name,
            "price": price,
            "service": service
        };
    }

    function editForm(id)
    {
        let name = document.querySelector('#insert1').value;
        let price = document.querySelector('#insert2').value;
        let service = document.querySelector('#insert3').checked;

        return {
            "id" : id,
            "name" : name,
            "price": price,
            "service": service
        };
    }

    function getAllShipmentAPI(pageNumber = 1)
    {
        const url = `http://localhost:8081/api/v1/shipment?pageNumber=${pageNumber}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.send();
        request.onload = function()
        {
            bindingShipment(request.response);
        }
    }

    function addNewShipmentAPI()
    {
        const url = `http://localhost:8081/api/v1/shipment/insert`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(addNewForm()));
        request.onload = function()
        {
            location.reload();
        }
    }

    function editShipmentAPI(id)
    {
        const url = `http://localhost:8081/api/v1/shipment/edit`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(editForm(id)));
        request.onload = function()
        {
            location.reload();
        }
    }

    function stopServiceAPI(id)
    {
        const url = `http://localhost:8081/api/v1/shipment/stop=${id}`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.setRequestHeader("Content-Type", "application/json");
        request.send();
        request.onload = function()
        {
            location.reload();
        }
    }

    function deleteShipmentAPI(id)
    {
        const url = `http://localhost:8081/api/v1/shipment/delete=${id}`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader('Authorization', localStorage.getItem('token'));
        request.setRequestHeader("Content-Type", "application/json");
        request.send();
        request.onload = function()
        {
            location.reload();
        }
    }
})();