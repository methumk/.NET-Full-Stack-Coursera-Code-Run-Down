// If else statement
let x = 10
if (x > 5)
{
    console.log(`x is greater than 5 @ value ${x}`)
}else
{
    console.log(`x is less than or equal to 5 @ value ${x}`)
}

x = 3
if (x > 5)
{
    console.log(`x is greater than 5 @ value ${x}`)
}else
{
    console.log(`x is less than or equal to 5 @ value ${x}`)
}


// Switch statement
let y = 'a'
switch(y)
{
    case 0:
        console.log(`Value is 0`)
        break
    case 'a':
        console.log(`Value is 'a'`)     // If you don't have a break it will go to next case
    case 'string':
        console.log('Value is also a string')
        break;
    default:
        console.log('Undefined')
        break
}