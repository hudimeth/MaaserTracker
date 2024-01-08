import { Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Dialog, DialogTitle, DialogContent, DialogActions, TextField, Box, Skeleton } from '@mui/material';
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const ManageSourcesPage = () => {
    const [sources, setSources] = useState([]);
    const [open, setOpen] = useState(false);
    const [selectedSourceName, setSelectedSourceName] = useState('');
    const [sourceIdForEdit, setSourceIdForEdit] = useState(null);
    const [sourcesCantDelete, setSourcesCantDelete] = useState([]);

    useEffect(() => {
        loadSources();
    }, []);

    const loadSources = async () => {
        const { data: sourcesWithIncomes } = await axios.get('/api/maasertracker/getsourceidsthathaveincomes');
        setSourcesCantDelete(sourcesWithIncomes);
        const { data } = await axios.get('/api/maasertracker/getallsources')
        setSources(data);
    }

    const handleOpen = (incomeSource = null) => {
        setOpen(true);
        if (incomeSource) {
            setSourceIdForEdit(incomeSource.id);
            setSelectedSourceName(incomeSource.source);
        }
    };

    const handleClose = () => {
        setOpen(false);
        setSelectedSourceName('');
        setSourceIdForEdit(null);
    };

    const handleAddEdit = async () => {
        if (sourceIdForEdit) {
            await axios.post('/api/maasertracker/editsource', { id: sourceIdForEdit, sourceNameEdited: selectedSourceName });
        } else {
            await axios.post('/api/maasertracker/addsource', { sourceName: selectedSourceName })
        }
        await loadSources();
        handleClose();
    };

    const handleDelete = async (sourceId) => {
        await axios.post('/api/maasertracker/deletesource', { sourceId });
        loadSources();
    };

    if (!sources[0]) {
        return (
            <Container maxWidth='sm' sx={{ flexDirection: 'column', alignItems: 'center', mt: 20 }}>
                <Skeleton />
                <Skeleton />
                <Skeleton />
            </Container>
        )
    }

    return (
        <Container>
            <Box sx={{ display: 'flex', justifyContent: 'center', margin: '20px 0' }}>
                <Button onClick={() => handleOpen()} variant="contained" color="primary" sx={{ minWidth: '200px' }}>
                    Add Source
                </Button>
            </Box>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ fontSize: '18px', color: 'blue' }}>Source</TableCell>
                            <TableCell align="right" sx={{ fontSize: '18px', color: 'blue' }}>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {sources.map((incomeSource) =>
                            <TableRow key={incomeSource.id}>
                                <TableCell sx={{ fontSize: '18px' }}>{incomeSource.source}</TableCell>
                                <TableCell align="right" sx={{ fontSize: '18px' }}>
                                    <Button color="primary" variant="outlined" sx={{ margin: '0 5px' }} onClick={() => handleOpen(incomeSource)}>Edit</Button>
                                    <Button color="secondary"
                                        variant="outlined"
                                        sx={{ margin: '0 5px' }}
                                        onClick={() => handleDelete(incomeSource.id)}
                                        disabled={sourcesCantDelete.includes(incomeSource.id)}
                                    >Delete</Button>
                                </TableCell>
                            </TableRow>)}
                    </TableBody>
                </Table>
            </TableContainer>
            <Dialog open={open} onClose={handleClose} fullWidth maxWidth="md">
                <DialogTitle>{sourceIdForEdit ? 'Edit Source' : 'Add Source'}</DialogTitle>
                <DialogContent>
                    <TextField autoFocus margin="dense" label="Source" type="text" fullWidth value={selectedSourceName} onChange={(e) => setSelectedSourceName(e.target.value)} />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleAddEdit} color="primary">
                        {sourceIdForEdit ? 'Save' : 'Add'}
                    </Button>
                </DialogActions>
            </Dialog>
        </Container>
    );
}

export default ManageSourcesPage;
