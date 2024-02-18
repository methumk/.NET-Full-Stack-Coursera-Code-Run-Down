// Let Variables
{
    let name = 'Bob'    // mutable - blocked scope - only exists within block of code it was defined
    name = 'Dog'
    console.log(`Name is ${name}`)
}
let name;
console.log(`Name is ${name}`)  // Previous name doesn't exist in this scope


// Const Variables
// Immutable and blocked scope
{
    // const notValid;          // Error - Need to initialize value upon variable declaration
    const PI = 3.14;
    // PI = 2.5;                // Error - can't change value
    console.log(`PI: ${PI}`)
}
// console.log(`PI: ${PI}`)        // Error - Original pi not within scope



// Var variables
// Mutable and not blocked scope
// Considered outdated
{
    var varVar;
    varVar = 20;                    // Can initialize after declaration
    console.log(`var is ${varVar}`)
    varVar = 30;                    // Mutable
}
console.log(`var is ${varVar}`)     // Not blocked scope - exists outside of scope