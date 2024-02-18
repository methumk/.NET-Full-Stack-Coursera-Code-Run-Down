// Numbers
let x = 5
console.log(`x=${x} and is type: ${typeof x}`)  // 64 bit number

// Big int
let bigInt = 123919239129391293192391923912931923912931923912931923n;   // need n to indicate big int
console.log(`bigInt=${bigInt} and is type: ${typeof bigInt}`)


// String
let str = 'this is a string'
console.log(`str=${str} and is type: ${typeof str}`)


// Bool
let bool = false;
console.log(`bool=${bool} and is type: ${typeof bool}`)


// Null
let n = null;   // means empty value JS primitive - var has been assigned as null contains no value
console.log(`n=${n} and is type: ${typeof n}`)


// Undefined
let undef       // var was declared but not been assigned
console.log(`undef=${undef} and is type: ${typeof undef}`)


// Objects
let person = {
    name: 'Gorlog',
    age: 5777777
}
console.log(`person=${person} with name ${person.name} and age: ${person.age} and is type: ${typeof person}`)


// Symbol
// Guaranteed to be unique - won't collide with keys any other code might add to the object
// There is a global symbol registry
// Ever symbol.for('key') will always return same symbol
let id = Symbol('id');
let id2 = Symbol('id');     // New symbol created - Symbol('id') === Symbol('id) := false
console.log(`id = ${id.toString()} and is type: ${typeof id}`)
console.log(`id2 = ${id2.toString()} and is type: ${typeof id2}`)