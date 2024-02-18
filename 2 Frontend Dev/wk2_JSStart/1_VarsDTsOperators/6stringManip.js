// Concatenation - + operator or concat() method
let x = 'string1', y = 'string2'
console.log(`${x} + '_' + ${y} = ${x + '_' + y}`)
console.log(`${x}.concat("_", ${y}) = ${x.concat("_", y)}\n`)


// Substrings - doesn't modify original string
let substr = x.slice(2)             // Goes till end of string
let substrSlice = x.slice(2, 5)     // Does not include last index
console.log(`x.slice(2) = ${substr}`)
console.log(`x.slice(2, 5) = ${substrSlice}`)
console.log(`Original string is not modified x=${x}`)