import { promises as fs } from 'fs'

let code = await fs.readFile('all_in_one.bb.lua', 'utf8')

/*
import parser from 'luaparse'
let ast = parser.parse(code, {
    comments: false,
    luaVersion: '5.1',
})
await fs.writeFile('ast.json', JSON.stringify(ast, null, 4), 'utf8')
*/

//code = code
//.replace(/^(?! |\}|\w+BuildingBlocks).*\n/gm, '')
//.replace(/, '"$1": ')

let json = ''

code = code.replace(/^\w+ = \{[ \n]*?\}$/gm, '')

json += '[\n'
for(let [m, func_name, func_body] of code.matchAll(/^(\w+)BuildingBlocks = \{((?:.|\n)*?)^\}/gm)){
    let json_body = func_body.replace(/\["(\w+)"\] = (.*?)(,?)$/gm, (m, param, value, ending) => {
        if(value == '{' && ending == '');
        else if(value == "True") value = true
        else if(value == "False") value = false
        else if(value.startsWith('"') && value.endsWith('"'));
        else if(/^-?\d+(\.\d+)?$/.test(value));
        else value = '"$' + value + '$"'
        return `"${param}": ${value}${ending}`
    })
    
    let replaced
    do {
        replaced = false
        json_body = json_body.replace(/^(\s*)"SubBlocks": \{((?:.|\n)*?)^\1\}/gm, (m, s, body) => {
            replaced = true
            return s + '"SubBlocks": [' + body + s + ']'
        })
    } while(replaced)
    
    json += `{\n`
    json += `    "Function": "${func_name}",\n`
    json += `    "SubBlocks": [\n`
    json += `        ${json_body}`
    json += `    \n]`
    json += `\n}`
}
json += '\n]'

json = json.replace(/\{((?:[ \n]*(?:-?\d+(?:\.\d+)),)*[ \n]+)\}/gm, '[$1]')
json = json.replace(/\}\{/g, '},\n{')
json = json.replace(/,(?=[ \n]*?[\]\}])/g, '')

json = json.replace(/"\$\{\}\$"/g, '{}')
json = json.replace(/(?<="SubBlocks": )\{\}/g, '[]')

let obj = JSON.parse(json)
let obj2 = {}
for(let func of obj){
    let arr = obj2[func.Function] || []
    arr.push(...func.SubBlocks)
    obj2[func.Function] = arr
}
obj = obj2

json = JSON.stringify(obj, null, 4)

await fs.writeFile('code.json', json, 'utf8');