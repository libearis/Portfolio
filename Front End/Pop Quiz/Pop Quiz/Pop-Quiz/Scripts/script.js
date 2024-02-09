(()=>
{
    let candidateList = [];
    let passedCandidate;
    let initialQuestion = [
        'Question0', 'Question1', 'Question2', 'Question3', 'Question4', 'Question5', 
        'Question6', 'Question7', 'Question8', 'Question9', 'Question10',
        'Question11', 'Question12', 'Question13', 'Question14', 'Question15',
        'Question16', 'Question17', 'Question18', 'Question19', 'Question20'];
    let questionList = initialQuestion;
    let randomQuestion;
    continueTheOldList();

    function continueTheOldList()
    {
        let oldListConfirmation = confirm('Do you wish to continue the old list');
        if(oldListConfirmation)
        {
            candidateList = JSON.parse(localStorage.getItem('candidates'))
            let addNewPendingCandidate = confirm('Do you wish to add new pending candidate');
            if(addNewPendingCandidate)
            {
                alert('Please enter all candidates name one by one into the prompt window after');
                enterAllCandidates(candidateList);
            }
            else
            {
                timersetting(candidateList);
            }
        }
        else
        {
            alert('Please enter all candidates name one by one into the prompt window after');
            enterAllCandidates(candidateList);
        }
    }

    function addButtonListener(timerCountdown, candidateList)
    {
        let modalWrapper = document.querySelector('.modal-wrapper');
        let rightContainer = document.querySelector('.right-container');

        let questionParagraph = document.querySelector('#question');
        let sentence = document.querySelector('.sentence');

        let beginButton = document.querySelector('#begin-button');
        let correctButton = document.querySelector('#correct-button');
        let wrongButton = document.querySelector('#wrong-button');

        
        beginButton.addEventListener('click', function(event)
        {
            beginButton.style.display = 'none';
            correctButton.style.display = 'inline-block';
            wrongButton.style.display = 'inline-block';
            questionParagraph.textContent = questionList[randomQuestion];
            questionParagraph.style.display = 'block';

            let index = questionList.indexOf(questionList[randomQuestion]);
            if (index > -1) 
            { 
                questionList.splice(index, 1); 
            }
            if(questionList.length == 0)
            {
                questionList = initialQuestion;
            }
            sentence.style.display = 'none';
            console.log(randomQuestion);
        })

        correctButton.addEventListener('click', function(event)
        {
            beginButton.style.display = 'inline-block';
            correctButton.style.display = 'none';
            wrongButton.style.display = 'none';
            modalWrapper.style.display = 'none';
            questionParagraph.style.display = 'none';
            sentence.style.display = 'block';
            rightContainer.append(passedCandidate)
            passedCandidate.textContent = passedCandidate.textContent + ' - correct';

            letTheGameBegin(timerCountdown, candidateList);
        })

        wrongButton.addEventListener('click', function(event)
        {
            beginButton.style.display = 'inline-block';
            correctButton.style.display = 'none';
            wrongButton.style.display = 'none';
            modalWrapper.style.display = 'none';
            questionParagraph.style.display = 'none';
            sentence.style.display = 'block';
            passedCandidate.textContent = passedCandidate.textContent + ' - wrong';
            rightContainer.append(passedCandidate);

            letTheGameBegin(timerCountdown, candidateList);
        })    
    }

    function enterAllCandidates(candidateList)
    {
        let enterNewCandidate = prompt('Enter new candidate name');
        candidateList.push(enterNewCandidate);
        enteranotherCandidateagain(candidateList);
    }

    function enteranotherCandidateagain(candidateList)
    {
        let anotherCandidate = confirm('Do you wish to enter new candidate?');
        if(anotherCandidate)
        {
            enterAllCandidates(candidateList);
        }
        else
        {
            timersetting(candidateList)
            localStorage.setItem('candidates', JSON.stringify(candidateList));
        }
    }

    function timersetting(candidateList)
    {        
        let timerCountdown = prompt('How many seconds do you wish to set your timer');
        addButtonListener(timerCountdown, candidateList);
        
        let candidateContainer = document.querySelector('.left-container');
        for(let candidate of candidateList)
        {
            let candidateName = document.createElement('span');
            candidateName.textContent = candidate;
            candidateName.setAttribute('id', candidate);
            candidateContainer.append(candidateName);
        }
        letTheGameBegin(timerCountdown, candidateList)
    }

    function letTheGameBegin(timerCountdown, candidateList)
    {
        if(candidateList.length == 0)
        {
            alert('Game Over');
        }
        else
        {
            let secondsLeft = document.querySelector('#seconds-left');
            secondsLeft.textContent = timerCountdown;
            let timeInterval = setInterval(() => {
                timerCountdown--;
                secondsLeft.textContent = timerCountdown;
                if(timerCountdown == 0)
                {
                    randomQuestion = Math.floor(Math.random() * questionList.length);
                    candidateChoosen(candidateList);
                    clearInterval(timeInterval);
                }
            }, 1000);
        }
    }

    function candidateChoosen(candidateList)
    {
        let randomNumber = Math.floor(Math.random() * candidateList.length);
        let modalWrapper = document.querySelector('.modal-wrapper');
        let candidateName = document.querySelector('#candidate');
        candidateName.textContent = candidateList[randomNumber];
        modalWrapper.style.display = 'flex';

        passedCandidate = document.querySelector(`#${candidateList[randomNumber]}`);
        let index = candidateList.indexOf(candidateList[randomNumber]);
        if (index > -1) 
        { 
            candidateList.splice(index, 1); 
        }
    }
})()