// Strings can be represented by chars in single quotes, double quotes, or back ticks
let str1 = "Double quote string"
let str2 = 'Single quote string'
let str3 = `Back ticked quote contains value ${str1}`

console.log(`Str1: ${str1} | typeof str1: ${typeof str1}`)
console.log(`Str2: ${str2} | typeof str2: ${typeof str2}`)
console.log(`Str3: ${str3} | typeof str3: ${typeof str3}`)

// Length of string
console.log(`str1.length = ${str1.length}`)

// Indexing chars
console.log(`str1[1] = ${str1[1]}`)

// Changing char - Not possible
str1[1] = 'a'                       // Strings are immutable - this won't error out but the string won't change               
console.log(`str1 = ${str1}`)


str1 = 'New String'
console.log(`str1 = ${str1}`)       // String are reassignable


const cstring = 'This is const'
// cstring = "new string"           // Error - can't reassign const string
console.log(`Const string: ${cstring}`)