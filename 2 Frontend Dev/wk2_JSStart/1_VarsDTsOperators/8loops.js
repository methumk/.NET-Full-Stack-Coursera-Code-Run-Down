let x = ['val1', 'val2', 'val3']

// For loop
console.log("Normal for loop: ")
for (let i=0; i < x.length; ++i)
{
    console.log(`\tx[${i}]=${x[i]}`)
}

// For .. in loop
console.log("\nfor .. in loop: ")
for (const i in x)                  
{
    console.log(`\tx[${i}]=${x[i]}`)            // i in this case is still index variable
}


// For each loop
// Uses lambda expression, don't have to idx argument optional
console.log("\nfor each loop: ")
x.forEach((elem, idx) => {
    console.log(`\tx[${idx}] = ${elem}`)
})



// While loop
let a = 0
console.log("\nwhile loop a < 5: ")
while(a < 5){
    a++
    console.log(`\ta = ${a}`) 
}


// Do while loop
a = 0
console.log("\ndo while a > 0: ")   // Executes at leas tonce
do {
    a++
    console.log(`\ta = ${a}`) 
}while(a > 1)