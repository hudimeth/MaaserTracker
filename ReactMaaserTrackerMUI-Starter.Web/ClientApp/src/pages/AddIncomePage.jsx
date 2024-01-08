import React, { useState, useEffect } from 'react';
import { Container, TextField, Button, Autocomplete, Typography } from '@mui/material';
import dayjs from 'dayjs';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const AddIncomePage = () => {

    const navigate = useNavigate();

    const [sources, setSources] = useState([]);
    const [selectedSourceId, setSelectedSourceId] = useState({
        id: 0,
        source: ''
    });
    const [amount, setAmount] = useState('');
    const [date, setDate] = useState(new Date());

    useEffect(() => {
        const loadSources = async () => {
            const { data } = await axios.get('/api/maasertracker/getallsources');
            setSources(data);
        }
        loadSources();
    }, []);

    const onSelectedSourceChange = (event, value) => {
        setSelectedSourceId(value.id);
    }

    const onButtonClick = async () => {
        await axios.post('/api/maasertracker/addincome', { sourceId: selectedSourceId, amount, date });
        navigate('/income');
    }

    return (
        <Container maxWidth="sm" sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center', height: '80vh' }}>
            <Typography variant="h2" component="h1" gutterBottom>
                Add Income
            </Typography>
            <Autocomplete
                isOptionEqualToValue={(option, value) => option.value === value.value}
                onChange={onSelectedSourceChange }
                options={sources}
                getOptionLabel={(option) => option.source}
                fullWidth
                margin="normal"
                renderInput={(params) => <TextField {...params} label="Source" variant="outlined" />}
            />
            <TextField
                label="Amount"
                variant="outlined"
                type="number"
                InputProps={{ inputProps: { min: 0, step: 0.01 } }}
                fullWidth
                margin="normal"
                value={amount}
                onChange={e => setAmount(e.target.value)}
            />
            <TextField
                label="Date"
                type="date"
                name='date'
                value={dayjs(date).format('YYYY-MM-DD')}
                onChange={e => setDate(e.target.value)}
                renderInput={(params) => <TextField {...params} fullWidth margin="normal" variant="outlined" />}
            />
            <Button variant="contained" color="primary" onClick={onButtonClick}>Add Income</Button>
        </Container>
    );
}

export default AddIncomePage;
