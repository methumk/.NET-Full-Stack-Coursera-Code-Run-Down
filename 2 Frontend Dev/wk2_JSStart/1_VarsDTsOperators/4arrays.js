let x = [5, 'string', {name: 'bob', age: 69}, [20, Symbol('id').toString()]]    // Note if you don't included .toString() when printing x you will get an error
console.log(`x = [${x}] |  typeof x: ${typeof x}`)

x.push('grapes')
console.log(`Pushed grapes: x = [${x}]`)

x.pop()
console.log(`Popped grapes: x = [${x}]`)

x.id = 123;
console.log(`Added a member value to x:\n\tx = [${x}]\n\tx.id = ${x.id}\n\ttypeof x: ${typeof x}`)

console.log(`Nested array at index 3: x[3] = [${x[3]}]`)

console.log(`Nested objected at index 3: x[2] = {${x[2]}}`)