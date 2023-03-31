import { useDisclosure, Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, ModalFooter, Textarea } from "@chakra-ui/react"
import { useState } from "react";
import { FolderToRead } from "../genericFiles/FolderToRead";
type PropsDisplayEditFile = {
    fileObject: FolderToRead,
    folderParentDisplay :string
  };

export default function EditFile({fileObject,folderParentDisplay }: PropsDisplayEditFile) {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const [dataLines, setData]=useState<string>('');
    const [textToSave, setTextToSave]= useState('');
    const [startSaving, setStartSaving]=useState(false);
    const openAndLoad=()=>{
      setData('');
      setTextToSave('');
      setStartSaving(false);  
      onOpen();
        const fetchLines = (): Promise<string> =>
            fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/GetFileText/'+ folderParentDisplay + fileObject.name).then(
                (res) => res.text() 
            );

            fetchLines()
                .then(it=> {
                    setData(it);
                    setTextToSave(it);
                });
                

    }
    const Save = ()=>{
      setStartSaving(true);
      const fetchLines = (): Promise<string> =>
      fetch(''+process.env.REACT_APP_URL+'api/v1.0/File/SetFileText/',
      {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({pathFile:folderParentDisplay + fileObject.name, content: textToSave})
      }
      ).then(
          (res) => res.text() 
      );

      fetchLines()
          .then(it=> {
              onClose();
          });
    }

    const handleMessageChange=(evt:any)=>{
        setTextToSave(evt.target.value);
        console.log(evt.target.value);
        console.log(dataLines);
    }

    return (
      <>
        <Button colorScheme='teal' onClick={openAndLoad}>Edit {fileObject.name}</Button>
  
        <Modal isOpen={isOpen} onClose={onClose} size={'full'}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Edit {fileObject.fullPath} </ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              {(dataLines.length===0) && <>Please wait, loading</>}

              {(dataLines.length>0) && 
              <> 
              <Textarea height={'700px'} value={textToSave} onChange={handleMessageChange} size={'lg'}   />
              </>
              }
            </ModalBody>
  
            <ModalFooter>
              {startSaving && <>Saving...</>}
              {!startSaving && 
              <>
              <Button colorScheme='blue' mr={3} onClick={onClose}>
                Close,do not save
              </Button>
              <Button colorScheme='red' onClick={Save}>Save!</Button>
              </>
              }
            </ModalFooter>
          </ModalContent>
        </Modal>
      </>
    )
  }