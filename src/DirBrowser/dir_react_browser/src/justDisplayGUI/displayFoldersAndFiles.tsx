import { TableContainer, Table, TableCaption, Thead, Tr, Th, Tbody, Td, Tfoot, } from "@chakra-ui/react";
import { FolderToRead } from "../genericFiles/FolderToRead";
import {Link } from 'react-router-dom';
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

    return <>
    
    <TableContainer>
  <Table variant='striped'>
    <TableCaption>this is data {allData?.length}</TableCaption>
    <Thead>
      <Tr>
        <Th>Nr</Th>
        <Th>Name</Th>
        <Th>IsFolder</Th>
        <Th isNumeric>Operations</Th>
      </Tr>
    </Thead>
    <Tbody>
       {allData.map((it,index) =>
        <Tr  key={it.id}>
        <Td>{index+1}</Td>
        <Td><Link to={folderParentDisplay + it.name}>{it.name}</Link> </Td>
        <Td>{it.isDirectory?"YES":"NO" }</Td>
        <Td>
            
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