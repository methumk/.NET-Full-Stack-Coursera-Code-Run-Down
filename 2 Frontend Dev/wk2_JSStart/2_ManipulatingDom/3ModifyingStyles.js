const div = document.querySelector('div')
const spanOne = document.getElementById('one')
const spanTwo = document.getElementById('two')


// Getting attributes values inside attribute
console.log(spanOne.getAttribute('name'))

// change the attribute
spanOne.setAttribute('name', 'dolla-bills-yall')

// Removes the attribute and value totally
spanOne.removeAttribute('name')

// You can also change the attribute directly
spanOne.id = '1'

// Create a class for span two or you can also use set attribute
spanTwo.classList.add("new-class")
// spanTwo.setAttribute('class', 'new-class')   // same functionality as above

// remove the class
spanTwo.classList.remove("new-class")


// Modifying styles
spanOne.style.color = 'red'
spanTwo.innerText = '\tThis span was changed by JS crew👽'
spanTwo.style.backgroundColor = 'green'
spanTwo.style.color = 'white'