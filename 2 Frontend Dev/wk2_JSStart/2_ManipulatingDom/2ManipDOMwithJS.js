// gets directory
// Lists all properties and methods you can use
console.dir(document)

// the domain - IP address
console.log(document.domain)

// Url
console.log(document.URL)

// Title
console.log(document.title)
document.title = "TITLE HAS BEEN CHANGED"
console.log(document.title)


// select the body
const body = document.body
body.append('APPENDED TO THE BODY')

// Document gets created but is not added into the document yet
const div = document.createElement('div')
console.log(div)
div.innerText = 'This div was added thru js'

// Div gets appended to the body
const firstDiv = document.getElementsByClassName('d')[0]    // first div
firstDiv.appendChild(div)

/* 
.append vs .appendChild

    - .append has same functionality as .appendChild()
    - but it returns nothing whereas appendChild returns appended node obj
    - can append several nodes and strings whereas appendChild can only do one
    - can append Node objects or string elements

*/
// firstDiv.append(div)


// remove the element
// Both methods work to remove an element from the DOM
firstDiv.removeChild(div)
// div.remove();