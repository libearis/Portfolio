(() => 
{
    getLoanAPI();
    buttonFunction();
    function buttonFunction()
    {
        let addLoanButton = document.querySelector('.add-loan');
        let closeFormButton = document.querySelector('.insert-new #close-form');
        let searchButton = document.querySelector('#search-button');

        addLoanButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let insertForm = document.querySelector('.insert-new');
            insertForm.classList.toggle('insert-new-active');

            let allOptions = document.querySelectorAll('option+option');
            for(let option of allOptions)
            {
                option.remove();
            }
            getInsertVMAPI();
        });

        closeFormButton.addEventListener('click', function(event)
        {
            let insertForm = document.querySelector('.insert-new');
            insertForm.classList.toggle('insert-new-active');
        });

        searchButton.addEventListener('click', function(event)
        {
            event.preventDefault();
            let search1 = document.querySelector('#search1');
            let search2 = document.querySelector('#search2');
            let search3 = document.querySelector('#available');

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

            getLoanAPI(1, search1.value, search2.value, search3.checked);
        });
    }

    function bindingIndex(json)
    {
        let indexLoan = JSON.parse(json);
        let tbody = document.querySelector('.main-tbody');
        for(let index = 0; index < indexLoan.loanDTOs.length; index++)
        {
            let tr = document.createElement('tr');
            tbody.append(tr);

            let tdButton = document.createElement('td');
            let buttonDetail = document.createElement('button');
            let buttonEdit = document.createElement('button');
            let buttonReturn = document.createElement('button');

            buttonDetail.textContent = 'Detail';
            buttonDetail.setAttribute('class', 'button-25');
            buttonEdit.textContent = 'Edit';
            buttonEdit.setAttribute('class', 'button-25');
            buttonReturn.textContent = 'Return';
            buttonReturn.setAttribute('class', 'button-25');

            let tdBookTitle = document.createElement('td');
            tdBookTitle.textContent = indexLoan.loanDTOs[index].book.title;
            let tdCustomerName = document.createElement('td');
            tdCustomerName.textContent = indexLoan.loanDTOs[index].customer.firstName + " " + indexLoan.loanDTOs[index].customer.lastName;
            let tdLoanDate = document.createElement('td');
            tdLoanDate.textContent = new Date(indexLoan.loanDTOs[index].loanDate).toLocaleDateString();
            let tdDueDate = document.createElement('td');
            tdDueDate.textContent = new Date(indexLoan.loanDTOs[index].dueDate).toLocaleDateString();
            let tdReturnDate = document.createElement('td');
            tdReturnDate.textContent = new Date(indexLoan.loanDTOs[index].returnDate??"").toLocaleDateString() == "Invalid Date"?
            "" : new Date(indexLoan.loanDTOs[index].returnDate).toLocaleDateString();

            tr.append(tdButton);
            tr.append(tdBookTitle);
            tr.append(tdCustomerName);
            tr.append(tdLoanDate);
            tr.append(tdDueDate);
            tr.append(tdReturnDate);
            tdButton.append(buttonDetail);
            tdButton.append(buttonEdit);
            tdButton.append(buttonReturn);

            buttonDetail.addEventListener('click', function(event)
            {
                let popupForm = document.querySelector('.detail-loan');
                let closeFormButton = document.querySelector('.detail-loan #close-form');
                popupForm.setAttribute('class', 'detail-loan-active');
                closeFormButton.addEventListener('click', function(event)
                {
                    popupForm.setAttribute('class', 'detail-loan');
                });

                let infoBook1 = document.querySelector('#info-book1');
                let infoBook2 = document.querySelector('#info-book2');
                let infoBook3 = document.querySelector('#info-book3');
                let infoBook4 = document.querySelector('#info-book4');
                let infoBook5 = document.querySelector('#info-book5');
                let infoBook6 = document.querySelector('#info-book6');

                infoBook1.textContent = indexLoan.loanDTOs[index].book.title;
                infoBook2.textContent = indexLoan.loanDTOs[index].book.categoryName;
                infoBook3.textContent = indexLoan.loanDTOs[index].book.authorName;
                infoBook4.textContent = indexLoan.loanDTOs[index].book.floor;
                infoBook5.textContent = indexLoan.loanDTOs[index].book.isle;
                infoBook6.textContent = indexLoan.loanDTOs[index].book.bay;

                let infoCustomer1 = document.querySelector('#info-customer1');
                let infoCustomer2 = document.querySelector('#info-customer2');
                let infoCustomer3 = document.querySelector('#info-customer3');
                let infoCustomer4 = document.querySelector('#info-customer4');
                
                infoCustomer1.textContent = indexLoan.loanDTOs[index].customer.membershipNumber;
                infoCustomer2.textContent = indexLoan.loanDTOs[index].customer.firstName + " " + indexLoan.loanDTOs[index].customer.lastName;
                infoCustomer3.textContent = indexLoan.loanDTOs[index].customer.phone;
                infoCustomer4.textContent = new Date(indexLoan.loanDTOs[index].customer.membershipExpireDate).toLocaleDateString();
            });

            buttonEdit.addEventListener('click', function(event)
            {
                let allOptions = document.querySelectorAll('option+option');
                for(let option of allOptions)
                {
                    option.remove();
                }
                let insertForm = document.querySelector('.insert-new');
                insertForm.classList.toggle('insert-new-active');
                getEditVMAPI(indexLoan.loanDTOs[index].id)
            });

            buttonReturn.addEventListener('click', function(event){
                if(new Date(indexLoan.loanDTOs[index].returnDate??"").toLocaleDateString() != "Invalid Date")
                {
                    alert("Book already returned");
                }
                else
                {
                    returnBookAPI(indexLoan.loanDTOs[index].id)
                }
            });
        }

        let pageCountDiv = document.querySelector('.tfoot-flex div');
        if(pageCountDiv.textContent == 'default')
        {
            pageCountDiv.textContent = `Page 1 of ${indexLoan.pagination.totalPages}`
        }

        for(let index = 1; index <= indexLoan.pagination.totalPages; index++)
        {
            let listPageDiv = document.querySelector('.tfoot-flex div+div');
            let pageButton = document.createElement('button');
            pageButton.setAttribute('class', 'button-25');
            pageButton.textContent = index;

            listPageDiv.append(pageButton);

            pageButton.addEventListener('click', function(event)
            {
                let search1 = document.querySelector('#search1');
                let search2 = document.querySelector('#search2');
                let search3 = document.querySelector('#available');
                let allTr = document.querySelectorAll('.main-tbody tr');
                let allPaginationButton = listPageDiv.querySelectorAll('button');
                pageCountDiv.textContent = `Page ${index} of ${indexLoan.pagination.totalPages}`;
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getLoanAPI(index, search1.value, search2.value, search3.checked);
            });
        }
    }

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();
    
        if (month.length < 2) 
            month = '0' + month;
        if (day.length < 2) 
            day = '0' + day;
    
        return [year, month, day].join('-');
    }

    function insertFormVM(json)
    {
        let insertVM = JSON.parse(json);
        let dropdownCustomer = document.querySelector('#input1');
        let dropdownBook = document.querySelector('#input2');
        for(let index = 0; index < insertVM.customers.length; index++)
        {
            let optionCustomer = document.createElement('option');
            optionCustomer.setAttribute('value', insertVM.customers[index].value);
            optionCustomer.textContent = insertVM.customers[index].text;

            dropdownCustomer.append(optionCustomer);
        }

        for(let index = 0; index < insertVM.books.length; index++)
        {
            let optionBook = document.createElement('option');
            optionBook.setAttribute('value', insertVM.books[index].value);
            optionBook.textContent = insertVM.books[index].text;
            dropdownBook.append(optionBook);
        }

        let submitInsert = document.querySelector('#submitinsert-new');
        submitInsert.addEventListener('click', function(event)
        {
            event.preventDefault();
            let validationBook = document.querySelector('.validation-book');
            let validationCustomer = document.querySelector('.validation-customer');
            if(dropdownCustomer.value == '')
            {
                if(dropdownBook.value == '')
                {
                    validationBook.textContent = 'Book cannot be empty';
                }
                else
                {
                    validationBook.textContent = '';
                }
                validationCustomer.textContent = 'Customer cannot be empty';
            }
            else if(dropdownBook.value == '')
            {
                validationBook.textContent = 'Book cannot be empty';
                validationCustomer.textContent = '';
            }
            else
            {
                postInsertNew();
            }
        });
    }

    function editForm(json)
    {
        let updateVM = JSON.parse(json);
        let dropdownCustomer = document.querySelector('#input1');
        let dropdownBook = document.querySelector('#input2');
        let loanDateInput = document.querySelector('#input3');
        let noteInput = document.querySelector('#input4');
        for(let index = 0; index < updateVM.customers.length; index++)
        {
            let optionCustomer = document.createElement('option');
            optionCustomer.setAttribute('value', updateVM.customers[index].value);
            optionCustomer.textContent = updateVM.customers[index].text;

            dropdownCustomer.append(optionCustomer);
        }

        for(let index = 0; index < updateVM.books.length; index++)
        {
            let optionBook = document.createElement('option');
            optionBook.setAttribute('value', updateVM.books[index].value);
            optionBook.textContent = updateVM.books[index].text;
            dropdownBook.append(optionBook);
        }

        dropdownCustomer.value = updateVM.customerNumber;
        dropdownBook.value = updateVM.bookCode;
        loanDateInput.value = formatDate(new Date(updateVM.loanDate));
        noteInput.value = updateVM.note;
        
        let submitInsert = document.querySelector('#submitinsert-new');
        submitInsert.addEventListener('click', function(event)
        {
            event.preventDefault();
            let validationBook = document.querySelector('.validation-book');
            let validationCustomer = document.querySelector('.validation-customer');
            if(dropdownCustomer.value == '')
            {
                if(dropdownBook.value == '')
                {
                    validationBook.textContent = 'Book cannot be empty';
                }
                else
                {
                    validationBook.textContent = '';
                }
                validationCustomer.textContent = 'Customer cannot be empty';
            }
            else if(dropdownBook.value == '')
            {
                validationBook.textContent = 'Book cannot be empty';
                validationCustomer.textContent = '';
            }
            else
            {
                postEdit(updateVM.id);
            }
        });
    }

    function customerSelectList()
    {
        return[
            {
            "disabled": true,
            "group": {
                "disabled": true,
                "name": "string"
            },
            "selected": true,
            "text": "string",
            "value": "string"
            }
        ]
    }

    function bookSelectList()
    {
        return[
            {
            "disabled": true,
            "group": {
                "disabled": true,
                "name": "string"
            },
            "selected": true,
            "text": "string",
            "value": "string"
            }
        ]
    }
    function insertLoan()
    {
        const customerNumber = document.querySelector('#input1').value;
        const bookCode = document.querySelector('#input2').value;
        const loanDate = document.querySelector('#input3').value;
        const note = document.querySelector('#input4').value;
        return{ 
                "customers": customerSelectList(),
                "books": bookSelectList(),
                'customerNumber': customerNumber,
                'bookCode': bookCode,
                'loanDate': loanDate,
                'note': note
            }
    }

    function editLoan(id)
    {
        const customerNumber = document.querySelector('#input1').value;
        const bookCode = document.querySelector('#input2').value;
        const loanDate = document.querySelector('#input3').value;
        const note = document.querySelector('#input4').value;
        return{ 
                "id" : id,
                "customers": customerSelectList(),
                "books": bookSelectList(),
                'customerNumber': customerNumber,
                'bookCode': bookCode,
                'loanDate': loanDate,
                'note': note
            }
    }

    function getLoanAPI(pagenumber = 1, bookTitle="", customerName ="", passedDueDate=false)
    {
        const url = `http://localhost:8000/api/v1/loan?pageNumber=${pagenumber}&pageSize=5&bookTitle=${bookTitle}&customerName=${customerName}&passedDueDate=${passedDueDate}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingIndex(request.response);
        }
    }

    function getInsertVMAPI()
    {
        const url = `http://localhost:8000/api/v1/loan/insertVM`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            insertFormVM(request.response);
        }
    }

    function postInsertNew()
    {
        const url = `http://localhost:8000/api/v1/loan/insert-loan`;
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertLoan()));
        request.onload = function()
        {
            location.reload();
        }
    }

    function getEditVMAPI(id)
    {
        const url = `http://localhost:8000/api/v1/loan/updateVM${id}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            editForm(request.response);
        }
    }

    function postEdit(id)
    {
        const url = `http://localhost:8000/api/v1/loan/update-loan`;
        let request = new XMLHttpRequest();
        request.open('PUT', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(editLoan(id)));
        request.onload = function()
        {
            location.reload();
        }
    }

    function returnBookAPI(id)
    {
        const url = `http://localhost:8000/api/v1/loan/return-${id}`;
        let request = new XMLHttpRequest();
        request.open('PATCH', url);
        request.send();
        request.onload = function()
        {
            location.reload();
        }
    }

})();