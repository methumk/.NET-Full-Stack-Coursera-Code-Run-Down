// Gets one element by the ID
console.log(document.getElementById('first'))

// Gets all elements belonging to a class
let greenlist = document.getElementsByClassName('green')
console.log(greenlist)

// Gets elements by tag type
let liList = document.getElementsByTagName('li')
console.log(liList)

// Returns FIRST element that MATCHES first css selector
let querySelect = document.querySelector('.green')
console.log(querySelect)


// Select all elements that match css selector
let matchSelect = document.querySelectorAll('.green')
console.log(matchSelect)