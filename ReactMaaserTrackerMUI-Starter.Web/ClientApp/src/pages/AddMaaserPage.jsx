import React, { useState } from 'react';
import { Container, TextField, Button, Typography } from '@mui/material';
import dayjs from 'dayjs';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


const AddMaaserPage = () => {

    const [maaserPayment, setMaaserPayment] = useState({
        recipient: '',
        amount:'',
        date:new Date()
    });

    const navigate = useNavigate();

    const editMaaserInputs = e => {
        const copy = { ...maaserPayment };
        copy[e.target.name] = e.target.value;
        setMaaserPayment(copy);
    }

    const AddMaaser = async () => {
        await axios.post('api/maasertracker/addmaaserpayment', { ...maaserPayment, amount: parseInt(maaserPayment.amount) });
        navigate('/maaser')
    }

    return (
        <Container maxWidth="sm" sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center', height: '80vh' }}>
            <Typography variant="h2" component="h1" gutterBottom>
                Add Maaser
            </Typography>
            <TextField label="Recipient" value={maaserPayment.recipient} onChange={editMaaserInputs} name='recipient' variant="outlined" fullWidth margin="normal" />
            <TextField label="Amount" value={maaserPayment.amount} onChange={editMaaserInputs} name='amount' variant="outlined" fullWidth margin="normal" />
            <TextField
                label="Date"
                type="date"
                name='date'
                value={dayjs(maaserPayment.date).format('YYYY-MM-DD')}
                onChange={editMaaserInputs}
                renderInput={(params) => <TextField {...params} fullWidth margin="normal" variant="outlined" />}
            />
            <Button variant="contained" onClick={AddMaaser} color="primary">Add Maaser</Button>
        </Container>
    );
}

export default AddMaaserPage;
