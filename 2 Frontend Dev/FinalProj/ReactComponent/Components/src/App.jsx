import { useRef, useState, useEffect, useContext, createContext } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

const bears = {
  "None"    : "https://wallpapers.com/images/hd/blank-white-background-xbsfzsltjksfompa.jpg",
  "Brown"   : "https://i.natgeofe.com/n/33b21bbe-a47d-45ba-a238-07bf7f182103/brown-bear_thumb_3x4.jpg",
  "Black"   : "https://d3m7xw68ay40x8.cloudfront.net/assets/2023/05/JUN23-NCIcons_Black-Bear-Portrait__Neil-Jernigan-560x690.jpg",
  "Polar"   : "https://as1.ftcdn.net/v2/jpg/04/16/98/92/1000_F_416989249_moEdVUxt4KnEoZ0XdlPYsvmfaCFdI6lu.jpg",
  "Grizzly" : "https://discovery.sndimg.com/content/dam/images/discovery/fullset/2021/9/9/GettyImages-1166676830.jpg.rend.hgtvcom.616.770.suffix/1631291183511.jpeg",
  "Panda"   : "https://media.istockphoto.com/id/1219403220/photo/closeup-shot-of-a-giant-panda-bear.jpg?s=612x612&w=0&k=20&c=B9gALQf9JfyavI5Ed6aSvdPgah-vJCe16SQWa1U5XCc=",
  "Sun"     : "https://assets-global.website-files.com/5adf752e38b7222e27f146ee/6140ddc58b298f4c917b01f2_sun-bear-inset-3.jpg",
}

const pages = {
  "Brown" : "Brown_bear",
  "Black" : "American_black_bear",
  "Polar" : "Polar_bear",
  "Grizzly" : "Grizzly_bear",
  "Panda" : "Giant_panda",
  "Sun" : "Sun_bear",
}

const sections = {
  "Brown" : "Description",
  "Black" : "Description",
  "Polar" : "Taxonomy",
  "Grizzly" : "Classification",
  "Panda" : "Description",
  "Sun" : "Characteristics",
}

const brackrgx = /\[\[(?=File)[^\]]*\]+/gi;
    const anglergx = /\<[^\>]*\>/gi;
    const sqigrgx = /\{\{(?!cvt|convert)[^\}]*\}+/gi;
    const rmvbrckrgx = /(\[\[)|(\]\])/g
    const convrgx = /(\{\{convert\|)|(\|\w+\|sigfig=\d+)|(\|\w+\|abbr=on)|(\}\})/gi
    const cvtrgx = /(\{\{cvt\|)/gi
    const barrgx = /\|/g
    const head3rgx = /(==)([A-Za-z0-9\s'"]+)(==)/gi
    const head4rgx = /(===)([A-Za-z0-9\s'"]+)(===)/gi
    const head5rgx = /(====)([A-Za-z0-9\s'"]+)(====)/gi

const getSectionsURL= (page) => {return `https://en.wikipedia.org/w/api.php?origin=*&action=parse&format=json&page=${page}&prop=sections&disabletoc=1`}
const getSectionDataURL = (page, sectionIdx) => {return `https://en.wikipedia.org/w/api.php?origin=*&action=parse&format=json&page=${page}&prop=wikitext&section=${sectionIdx}&disabletoc=1`}

const BearContext = createContext(null);


function BearFace() {
  const {
    currBear,
    setCurrBear
  } = useContext(BearContext)

  function bearTitle(bear) {
    if (bear === "None")
    {
      return "Select a Bear to Start"
    }
    return `The ${bear} Bear`
  }

  return (
    <>
      <div id="curr-bear">
        <img src={bears[currBear]} id="curr-bear-img" alt="Bear Profile"/>
      </div>
      <div id="curr-bear-header"><b>{bearTitle(currBear)}</b></div>
    </>
  );
}


function BearSelect(){
  const {
    currBear,
    setCurrBear
  } = useContext(BearContext)

  return (
    <div id="bear-cntr">
      <div id="bear-selector">
        <select name="bears-select" id="allbears" value={currBear} onChange={e => setCurrBear(e.target.value)}>
          <option value="None">None Selected</option>
          <option value="Brown">Brown Bear</option>
          <option value="Black">Black Bear</option>
          <option value="Polar">Polar Bear</option>
          <option value="Grizzly">Grizzly Bear</option>
          <option value="Panda">Panda Bear</option>
          <option value="Sun">Sun Bear</option>
        </select>
      </div>
      <div id="bear-data">

      </div>
    </div>
  );
}


function BearDescrip(){
  const {
    currBear,
    setCurrBear
  } = useContext(BearContext)
  const bearDataRef = useRef("")

  useEffect(() => {
    if (currBear !== "None")
    {
      fetch(getSectionsURL(pages[currBear]))
        .then(response => response.json())
        .then(json => {
          let sectionData = json.parse.sections.find((e) => e.line === sections[currBear])
          let idx = sectionData.index

          fetch(getSectionDataURL(pages[currBear], idx))
            .then(response => response.json())
            .then(data => {
              let info = data.parse.wikitext["*"]
              let infocleaned = info
                  .replaceAll(brackrgx, "")
                  .replaceAll(anglergx, "")
                  .replaceAll(sqigrgx, "")
                  .replaceAll(rmvbrckrgx, "")
                  .replaceAll(convrgx, "")
                  .replaceAll(cvtrgx, "")
                  .replaceAll(head5rgx, "<h5>$2</h5>")
                  .replaceAll(head4rgx, "<h4>$2</h4>")
                  .replaceAll(head3rgx, "<h3>$2</h3>")
                  .replaceAll(barrgx, " ")
              console.log(infocleaned)
              bearDataRef.current.innerHTML = infocleaned
            })
            .catch(error => console.error(error));
        })
        .catch(error => console.error(error));
    }
  }, [currBear]);

  return (
    <>
    <div id="bear-data" ref={bearDataRef}></div>
    </>
  );
}


function App() {
  const [currBear, setCurrBear] = useState("None")

  return (
    <BearContext.Provider value={{
      currBear,
      setCurrBear
    }}>
      <BearFace />
      <BearSelect />
      <BearDescrip />
    </BearContext.Provider>
  )
}

export default App
