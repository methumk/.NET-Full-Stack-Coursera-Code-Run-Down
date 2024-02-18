// Non returning function
// No type specification, but can do it if you are in typescript
// Returns undefined by default
function myFunc(name, age)
{
    console.log(`This function found user: ${name} who is ${age}`)
}
let ret = myFunc('bob', 62)
console.log(`myFunc('Bob', 62) returned ${ret}\n`)


// Returning function
function myFuncRet(name, age)
{
    console.log(`This function created user: ${name} who is ${age}`)
    return {name: name, age: age}
}
ret = myFuncRet('bob', 62)
console.log(`myFuncRet('Bob', 62) returned ${ret}\n`)


// Functions using globals
let GLOB = 'THIS IS A GLOBAL'
function printGlob()
{
    console.log(`Function calls global: ${GLOB}\n`)
}
printGlob()


// Local let/var function variables
function localVars()
{
    let a = "let_a local variable"
    var b = "let_b local variable"

    console.log(`Variable a inside func: ${a}`)
    console.log(`Variable b inside func: ${b}`)
}
// console.log(`Variable a outside func: ${a}`)     Error - let is within scope only
// console.log(`Variable b outside func: ${b}`)     Error - var has to be within function scope/regular scope this would be fine



// Reference/Value passing
// Objects and arrays always passed by reference
let arr = ['val1', 'val2', 'val3']
let obj = {name: 'bob', age: 62}

// Changing value inside object
function refPass(arr, obj)
{
    console.log(`\n\t\tInside Call by Reference Method - change value`);
    arr[1] = 0;
    obj.name = "Andrei"
    console.log(`\t\t\tarr: ${arr}`);
    console.log(`\t\t\tobj.name = ${obj.name} | obj.age = ${obj.age}\n`);
}

// Reassigning object
function refPassReassign(arr, obj)
{
    console.log("\n\t\tInside Call by Reference Method - reassign");
    arr = 'arr_reassigned'
    obj = {name: 'INVALID_NAME', age: 'INVALID_AGE'}
    console.log(`\t\t\tarr: ${arr}`);
    console.log(`\t\t\tobj.name = ${obj.name} | obj.age = ${obj.age}\n`);
}

// Testing changing value inside
console.log(`Outside BEFORE changing value inside:\n\tarr: ${arr}\n\tobj.name = ${obj.name} | obj.age = ${obj.age}`);
refPass(arr, obj)
console.log(`Outside AFTER changing value inside:\n\tarr: ${arr}\n\tobj.name = ${obj.name} | obj.age = ${obj.age}`);
console.log('======================================================================\n')

// Reassigning object/array inside function doesn't change original
arr = ['val1', 'val2', 'val3']
obj = {name: 'bob', age: 62}
console.log(`Outside BEFORE reassigning inside:\n\tarr: ${arr}\n\tobj.name = ${obj.name} | obj.age = ${obj.age}`);
refPassReassign(arr, obj)
console.log(`Outside AFTER reassigning inside:\n\tarr: ${arr}\n\tobj.name = ${obj.name} | obj.age = ${obj.age}`);
console.log('======================================================================\n')



// Value passing 
// Only occur for js primitives
// Values are copied - value outside doesn't change
let a = 10
let b = 'str'

function primValue(a, b)
{
    console.log("\n\t\tInside Call by Value Method");
    a = 0
    b = 'dog'
    console.log(`\t\t\ta=${a}, b=${b}\n`)
}
console.log(`Outside BEFORE reassigning inside:\n\ta=${a}\n\tb=${b}`);
primValue(a, b)
console.log(`Outside AFTER reassigning inside:\n\ta=${a}\n\tb=${b}`);
