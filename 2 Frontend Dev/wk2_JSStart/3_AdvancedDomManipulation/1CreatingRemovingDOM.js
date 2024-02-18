const neighButton = document.querySelector('#neigh')
const noOxButt = document.querySelector('#no-ox')
const foodList = document.getElementById('food-list')


neighButton.addEventListener('click', () => {
    const newListItem = document.createElement('li')
    const newListText = document.createTextNode('Horse 🐎')

    // Append horse text to new item list
    newListItem.append(newListText)
    // Append horse item to food list
    foodList.append(newListItem)
})


noOxButt.addEventListener('click', () => {
    const ox = document.querySelector('li:nth-child(2)')
    foodList.removeChild(ox)
})