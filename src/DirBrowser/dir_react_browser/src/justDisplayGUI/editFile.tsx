import { useDisclosure, Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, Textarea } from "@chakra-ui/react"
import { useQuery } from "@tanstack/react-query";
import { useState } from "react";
import { FolderToRead } from "../genericFiles/FolderToRead";
type PropsDisplayEditFile = {
    fileObject: FolderToRead,
    folderParentDisplay :string
  };

export default function EditFile({fileObject,folderParentDisplay }: PropsDisplayEditFile) {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const [dataLines, setData]=useState<string[]>([]);
    const [textToSave, setTextToSave]= useState('');
    const openAndLoad=()=>{
        onOpen();
        const fetchLines = (): Promise<string[]> =>
            fetch('http://localhost:5288/api/v1.0/File/GetFileLines/'+ folderParentDisplay + fileObject.name).then(
                (res) => res.json() 
            );

            fetchLines()
                .then(it=> {
                    setData(it);
                    setTextToSave(it.join('\r\n'));
                });
                

    }
    const handleMessageChange=(evt:any)=>{
        setTextToSave(evt.target.value);
        console.log(evt.target.value);
    }

    return (
      <>
        <Button colorScheme='teal' onClick={openAndLoad}>Edit {fileObject.name}</Button>
  
        <Modal isOpen={isOpen} onClose={onClose} size={'full'}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Edit {fileObject.fullPath} : Number lines: {dataLines.length}</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              {(dataLines.length===0) && <>Please wait, loading</>}

              {(dataLines.length>0) && 
              <> {(dataLines.join('\r\n') !== textToSave)?"Modified":""}
              <Textarea value={textToSave} onChange={handleMessageChange}  />
              {textToSave}
              </>
              }
            </ModalBody>
  
            <ModalFooter>
              <Button colorScheme='blue' mr={3} onClick={onClose}>
                Close,do not save
              </Button>
              <Button variant='ghost'>Save!</Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </>
    )
  }