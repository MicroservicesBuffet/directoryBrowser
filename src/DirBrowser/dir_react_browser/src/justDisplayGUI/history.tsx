import { useDisclosure, Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, Textarea,  Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr } from "@chakra-ui/react"
import { useState } from "react";
import { FolderToRead } from "../genericFiles/FolderToRead";
import { historyFile } from "../genericFiles/historyFile";
type PropsDisplayEditFile = {
    fileObject: FolderToRead,
    folderParentDisplay :string
  };

export default function HistoryFile({fileObject,folderParentDisplay }: PropsDisplayEditFile) {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const [data, setData]=useState<historyFile[]|null>(null);
    const openAndLoad=()=>{
        onOpen();
        const fetchLines = (): Promise<historyFile[]> =>
            fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/GetFileHistory/'+ folderParentDisplay + fileObject.name).then(
                (res) => res.json() 
            );

            fetchLines()
                .then(it=> {
                    setData(it);
                });
                

    }


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
      </Tr>
    </Thead>
    <Tbody>
       {data.sort((a,b)=>b.lastModified.valueOf()- a.lastModified.valueOf()).map((it,index) =>
        <Tr  key={index}>
        <Td>{index+1}</Td>
        <Td><>
          Modified by <b>{it.user}</b> at {new Date(it.lastModified).toLocaleString()} UTC
          </></Td>
        
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