import { TableContainer, Table, TableCaption, Thead, Tr, Th, Tbody, Td, Tfoot, Button, Box, } from "@chakra-ui/react";
import { FolderToRead } from "../genericFiles/FolderToRead";
import {Link } from 'react-router-dom';
import EditFile from "./editFile";
import HistoryFile from "./history";
type PropsDisplayFoldersAndFiles = {
    allData: FolderToRead[],
    folderParent:string
  };
export default function DisplayFoldersAndFiles(
    { allData, folderParent }: PropsDisplayFoldersAndFiles
)
{
    allData =allData.sort((a,b)=>a.name.localeCompare(b.name));
    const files=allData.filter(it=>!it.isDirectory);
    const dirs=allData.filter(it=>it.isDirectory);
    const folderParentDisplay='/show/'+ (folderParent?folderParent+'/':'');
    const downloadFile =(path:string)=>{
      var url =''+process.env.REACT_APP_URL+'api/v1.0/File/GetFileContent/';
      window.open(url+folderParent+'/'+path);
    }

    return <>
    
    <TableContainer>
  <Table variant='striped'>
    <TableCaption>{(folderParent.length>0) && <> Content of {folderParent}</>}</TableCaption>
    <Thead>
      <Tr>
        <Th>Nr</Th>
        <Th>Name</Th>
        <Th>Details</Th>        
        <Th >Operations</Th>
      </Tr>
    </Thead>
    <Tbody>
       {allData.map((it,index) =>
        <Tr  key={it.id}>
        <Td>{index+1}</Td>
        <Td>
        {
        it.isDirectory && 
          <Box bg='pink.100'>
          <Link to={folderParentDisplay + it.name}>{it.name}</Link> 
          </Box>
        }
        {
          (!it.isDirectory) && 
          <>{it.name}</>
        }
        </Td>
        <Td><>
          {it.isDirectory?"Folder":"File" }  <br />      
          {it.fullPath}<br />
          Modified at {it.lastModified}<br />
          {!it.isDirectory && <>Size {it.length}</>}
          </>
        </Td>
        <Td>
            {it.isDirectory &&
            <Button colorScheme='teal'>
              <Link to={folderParentDisplay + it.name}>GoTo</Link>
              </Button>
            }
            {!it.isDirectory && 
            <>
              <Button onClick={()=>downloadFile(it.name)} colorScheme='blue'>Download</Button>
              <hr />
              <HistoryFile fileObject={it} folderParentDisplay={folderParentDisplay.replace('/show/','')}></HistoryFile> 
              <hr />
              <EditFile fileObject={it} folderParentDisplay={folderParentDisplay.replace('/show/','')}></EditFile>
              </>
             }
            
        </Td>
      </Tr>
       )
       } 
      
    </Tbody>
    <Tfoot>
      <Tr>
        <Th>Files:{files.length}</Th>
        <Th>Folders:{dirs.length}</Th>
        <Th >Total:{allData.length}</Th>
      </Tr>
    </Tfoot>
  </Table>
</TableContainer>


    </>
}