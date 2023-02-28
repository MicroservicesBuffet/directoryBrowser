import { TableContainer, Table, TableCaption, Thead, Tr, Th, Tbody, Td, Tfoot } from "@chakra-ui/react";
import { FolderToRead } from "../genericFiles/FolderToRead";

type PropsDisplayFoldersAndFiles = {
    allData: FolderToRead[],
  };
export default function DisplayFoldersAndFiles(
    { allData }: PropsDisplayFoldersAndFiles
)
{
    const files=allData.filter(it=>!it.isDirectory);
    const dirs=allData.filter(it=>it.isDirectory);
    

    return <>
    
    <TableContainer>
  <Table variant='striped'>
    <TableCaption>this is data {allData?.length}</TableCaption>
    <Thead>
      <Tr>
        <Th>Name</Th>
        <Th>IsFolder</Th>
        <Th isNumeric>Operations</Th>
      </Tr>
    </Thead>
    <Tbody>
       {allData.map(it =>
        <Tr>
        <Td>{it.name}</Td>
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