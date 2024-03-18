(() =>{
    getBookAPI();
    initiateButton();

    function initiateButton()
    {
        let closeFormButton = document.querySelector('#close-form');
        let searchButton = document.querySelector('#search-button');
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

            getBookAPI(localStorage.getItem("categoryName"), 1, search1.value, search2.value, search3.checked);
        });
    }
    function bindingIndex(bookJSON)
    {
        let indexBookDTO = JSON.parse(bookJSON); 
        let bookDTO = JSON.parse(bookJSON).books;
        let mainTbody = document.querySelector('.main-tbody');

        let pListOf = document.querySelector('p');
        let addBook = document.querySelector('.add-book');
        pListOf.textContent = `List of ${indexBookDTO.categoryName} Books`;
        addBook.textContent = `Add New ${indexBookDTO.categoryName} book`;

        addBook.addEventListener('click', function(event)
        {
            location.href = `insert-${indexBookDTO.categoryName}`;
        });

        for(let index = 0; index < bookDTO.length; index++)
        {
            let tr = document.createElement('tr');
            mainTbody.append(tr);

            let tdButton = document.createElement('td');
            let buttonEdit = document.createElement('button');
            let buttonDelete = document.createElement('button');

            buttonEdit.textContent = 'Edit';
            buttonEdit.setAttribute('class', 'button-25');
            buttonDelete.textContent = 'Delete';
            buttonDelete.setAttribute('class', 'button-25');

            let tdBookTitle = document.createElement('td');
            tdBookTitle.textContent = bookDTO[index].title;
            let tdAuthorName = document.createElement('td');
            tdAuthorName.textContent = bookDTO[index].authorName;
            let tdIsBorrowed = document.createElement('td');
            tdIsBorrowed.textContent = bookDTO[index].isBorrowed == false?"Available" : "Borrowed";
            let tdReleaseDate = document.createElement('td');
            tdReleaseDate.textContent = new Date(bookDTO[index].releaseDate).toLocaleDateString();
            let tdTotalPage = document.createElement('td');
            tdTotalPage.textContent = bookDTO[index].totalPage;

            tr.append(tdButton);
            tr.append(tdBookTitle);
            tr.append(tdAuthorName);
            tr.append(tdIsBorrowed);
            tr.append(tdReleaseDate);
            tr.append(tdTotalPage);
            tdButton.append(buttonEdit);
            tdButton.append(buttonDelete);
            if(bookDTO[index].summary != null)
            {
                let buttonSummary = document.createElement('button');
                buttonSummary.setAttribute('class', 'button-25')
                buttonSummary.textContent = 'Summary'
                tdButton.append(buttonSummary);
                buttonSummary.addEventListener('click', function(event){
                    let popupForm = document.querySelector('.insert-new');
                    popupForm.setAttribute('class', 'insert-new-active');

                    let summaryText = document.querySelector('#summary-text');
                    summaryText.textContent = bookDTO[index].summary;
                });
            }

            buttonEdit.addEventListener('click', function(event)
            {
                location.href = `update-${bookDTO[index].code}`;
            });

            buttonDelete.addEventListener('click', function(event)
            {
                location.href = `delete-${bookDTO[index].code}`;
            });
        }

        let pageCountDiv = document.querySelector('.tfoot-flex div');
        if(pageCountDiv.textContent == 'default')
        {
            pageCountDiv.textContent = `Page 1 of ${indexBookDTO.pagination.totalPages}`
        }

        for(let index = 1; index <= indexBookDTO.pagination.totalPages; index++)
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
                pageCountDiv.textContent = `Page ${index} of ${indexBookDTO.pagination.totalPages}`;
                for(let tr of allTr)
                {
                    tr.remove();
                }
                for(let button of allPaginationButton)
                {
                    button.remove();
                }
                getBookAPI(localStorage.getItem("categoryName"), index, search1.value, search2.value, search3.checked);
            });
        }
    }
    function getBookAPI(categoryName = localStorage.getItem("categoryName"),pagenumber = 1, title ="", authorName="", IsAvailable = false)
    {
        const url = `http://localhost:8000/api/v1/Book/${categoryName}?pageNumber=${pagenumber}&pageSize=5&title=${title}&author=${authorName}&IsAvailable=${IsAvailable}`;
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.send();
        request.onload = function()
        {
            bindingIndex(request.response);
        }
    }
})();