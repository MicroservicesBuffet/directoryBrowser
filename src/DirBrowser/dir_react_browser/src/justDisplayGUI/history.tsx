import { useDisclosure, Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter,  Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr } from "@chakra-ui/react"
import { useState } from "react";
import { FolderToRead } from "../genericFiles/FolderToRead";
import { historyFile } from "../genericFiles/historyFile";
import ReactDiffViewer from 'react-diff-viewer';
type PropsDisplayEditFile = {
    fileObject: FolderToRead,
    folderParentDisplay :string
  };

//   const oldCode = `
// const a = 10
// const b = 10
// const c = () => console.log('foo')

// if(a > 10) {
//   console.log('bar')
// }

// console.log('done')
// `;
// const newCode = `
// const a = 10
// const boo = 10

// if(a === 10) {
//   console.log('bar')
// }
// `;

interface DataToCompare{
  file1:historyFile|null,
  file2:historyFile|null
}
export default function HistoryFile({fileObject,folderParentDisplay }: PropsDisplayEditFile) {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const [data, setData]=useState<historyFile[]|null>(null);
    const [dataToCompare, setDataToCompare]=useState<DataToCompare|null>(null);
    const openAndLoad=()=>{
        onOpen();
        const fetchLines = (): Promise<historyFile[]> =>
            fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/GetFileHistory/'+ folderParentDisplay + fileObject.name
            ,
    {
      method: 'GET',
      credentials: 'include' 
    }
            
            ).then(
                (res) => {
                  if(res.status === 200 )
                    return res.json() ;
                  if(res.status === 204)
                    return [];
                }
            );

            fetchLines()
                .then(it=> {
                   // console.log(it);
                   if(!it)
                    return;
                    var a=it.sort((a,b)=>b.dbId- a.dbId);
                   // console.log(a);
                    setData(a);
                });
                

    }
    const ExistCompare= ():boolean=>{
      console.log('asdasd', dataToCompare)
      if(dataToCompare == null)
        return false;
      if(dataToCompare.file1 == null)
        return false;
      if(dataToCompare.file2 == null)
        return false;
      if(dataToCompare.file1.content == null)
        return false;
      
      if(dataToCompare.file2.content == null)
        return false;

      return true;
    }
    const setCompare=(file:historyFile, nr:number)=>{
      if(file.content == null)
        fetch(process.env.REACT_APP_URL+'api/v1.0/File/GetFileHistory/'+file.dbId,{
          method: 'GET',
          credentials: 'include' 
        })
          .then(res=>res.text())
          .then(it=>{
            file.content=it;
            setDataToCompare((prev) => {
             
              if(prev == null)
                if(nr === 1)
                  return {file1:file, file2:null};
                else
                  return {file1:null, file2:file};                
              else
              if(nr === 1)
                return {file1:file, file2:prev.file2};
              else
              return {file1:prev.file1, file2:file};

                
            });  
          });

      if(nr === 1){
        if(dataToCompare == null){
          setDataToCompare({file1:file, file2:null});
        }else{
          setDataToCompare({file1:file, file2:dataToCompare.file2});
        }

      }
      if(nr === 2){
        if(dataToCompare == null){
          setDataToCompare({file1:null, file2:file});
        }else{
          setDataToCompare({file1:dataToCompare.file1, file2:file});
        }
        
      }
      
    }
    const downloadFile=(dbId:number)=>{
      window.open(process.env.REACT_APP_URL+'api/v1.0/File/GetFileHistory/'+dbId);
    };
    return (
      <>
        <Button colorScheme='teal' onClick={openAndLoad}>History {fileObject.name}</Button>
  
        <Modal isOpen={isOpen} onClose={onClose} size={'full'}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>History entries for {fileObject.fullPath} </ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              {(data === null) && <>Please wait, loading</>}

              {(data !== null) && 
              <> 
                  <TableContainer>
  <Table variant='striped'>
    <TableCaption>Number Items: {data.length}</TableCaption>
    <Thead>
      <Tr>
        <Th>Nr</Th>
        <Th>Name</Th>
        <Th>Operations</Th>
      </Tr>
    </Thead>
    <Tbody>
       {data.map((it,index) =>
        <Tr  key={index}>
        <Td>{data.length - index}</Td>
        <Td><>
          Modified by <b>{it.user}</b> at {new Date(it.lastModified).toLocaleString()} UTC
          </></Td>
        <Td>
          <Button onClick={()=>downloadFile(it.dbId)} colorScheme='blue'>Download</Button>
          <br /><br />
          {(dataToCompare?.file1 !== it) && <>
          <Button onClick={()=>setCompare(it,1)} colorScheme='blue'>
            Compare 1            
            </Button>    
            </>
            }
            {(dataToCompare?.file1 === it) && <span>Selected 1!</span>}

            {(dataToCompare?.file2 !== it) && <>
            
          <Button onClick={()=>setCompare(it,2)} colorScheme='blue'>
            Compare 2
           </Button>    
           </>}
           {(dataToCompare?.file2 === it) && <span>Selected 2!</span>}

      </Td>
      
      </Tr>
       )
       } 
      
    </Tbody>
    <Tfoot>
      <Tr>
        <Th>File {folderParentDisplay + fileObject.name}</Th>
        <Th>==</Th>
        <Th>--</Th>
      </Tr>
    </Tfoot>
  </Table>
</TableContainer>
{ExistCompare() && <>

<ReactDiffViewer oldValue={dataToCompare!.file1!.content} newValue={dataToCompare!.file2!.content} splitView={true}
leftTitle=  {dataToCompare!.file1!.user +" "+ new Date(dataToCompare!.file1!.lastModified).toLocaleString()}
rightTitle= {dataToCompare!.file2!.user +" "+ new Date(dataToCompare!.file2!.lastModified).toLocaleString()}
/>
</>
}
              </>
              }
            </ModalBody>
  
            <ModalFooter>
              <Button colorScheme='blue' mr={3} onClick={onClose}>
                Close
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </>
    )
  }