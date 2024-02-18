const butt = document.getElementById('butt')

let audio = new Audio('../content/power.mp3')
const playStart = 125
const playEnd = 126.197

// Click event listener
// Specify the type of event listener and then provide callback function
butt.addEventListener('click', () => {
    console.log('Clicked')

    // Play audio from current time
    audio.currentTime = playStart
    audio.play()

    // Time out audio
    audioId = setTimeout(() => {
        audio.pause()
    }, (playEnd - playStart)*1000)
})



// Keypress event listener
const search = document.querySelector('input#search')
const froggy = new Audio('../content/liver.mp3')
const frogStart = 153
const frogEnd = 156.5
// This also included function that checks the key that is pressed
search.addEventListener('keypress', (e) => {
    if (e.key == 'Enter')
    {
        if (search.value.toLowerCase() == 'frog')
        {
            console.log('found')
            document.body.style.backgroundImage = 'url(../content/dsc08012.webp)'
            
            froggy.currentTime = frogStart
            froggy.play()

            // Time out audio
            frogId = setTimeout(() => {
                froggy.pause()
                document.body.style.backgroundImage = "none" 
            }, (frogEnd - frogStart)*1000)
        }
    }
})