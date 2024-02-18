// Face functionality
let face = document.getElementById('face')
let faces = {
    0: '💀',
    1: '🤮',
    2: '🤢',
    3: '😱',
    4: '🤔',
    5: '😎',
}

// Health bar functionality
let health = 5;
const healthBarMaxWidth = document.getElementById('health-bar').getBoundingClientRect().width;
console.log('max width: ', healthBarMaxWidth)
let healthColor = {
    1: '#00ff08',
    .80: '#9eea42',
    .60: '#fc8200',
    .40: '#f4f400',
    .20: '#f42900',
    .0: '#000000'
}
function reduceHealth(percentReduct) {
    var healthValue = document.getElementById("health-value");
    var currWidth = document.getElementById('health-value').offsetWidth;
    var newWidth = currWidth - (healthBarMaxWidth * percentReduct);
    if (newWidth < 0) newWidth = 0;
    healthValue.style.width = newWidth + 'px';
    healthValue.style.backgroundColor = healthColor[Math.round(10*newWidth/healthBarMaxWidth)/10]
}



// Adding listener for each li item
const listItems = document.querySelectorAll('li')
listItems.forEach((item) => {
    // Attach listener to all li items
    item.addEventListener('click', () => {
        console.log(`Clicked: ${item.innerText}`)

        // toggle works like a switch turning the style on or off
        // Create new class attribute called completed
        item.classList.toggle('completed')

        // If tag is striked
        var completed = item.className === 'completed'

        // Update face and health
        switch(item.innerText)
        {
            case 'Eggs':
                if (completed)
                {
                    health--;
                    face.innerText = faces[health]
                    reduceHealth(1/5);
                }else {
                    health++;
                    face.innerText = faces[health]
                    reduceHealth(-1/5);
                }
                break;
            case 'Money':
                if (completed)
                {
                    health--;
                    face.innerText = faces[health]
                    reduceHealth(1/5);
                }else {
                    health++;
                    face.innerText = faces[health]
                    reduceHealth(-1/5);
                }
                break;
            case 'Affordable housing':
                if (completed)
                {
                    health--;
                    face.innerText = faces[health]
                    reduceHealth(1/5);
                }else {
                    health++;
                    face.innerText = faces[health]
                    reduceHealth(-1/5);
                }
                break;
            case 'Power':
                if (completed)
                {
                    health--;
                    face.innerText = faces[health]
                    reduceHealth(1/5);
                }else {
                    health++;
                    face.innerText = faces[health]
                    reduceHealth(-1/5);
                }
                break;
            case 'Gas':
                if (completed)
                {
                    health--;
                    face.innerText = faces[health]
                    reduceHealth(1/5);
                }else {
                    health++;
                    face.innerText = faces[health]
                    reduceHealth(-1/5);
                }
                break;
            default:
                face.innerText = '🤐';
                break;
        }
        
    })
})