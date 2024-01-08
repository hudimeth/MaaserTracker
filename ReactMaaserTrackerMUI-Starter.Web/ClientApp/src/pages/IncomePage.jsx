import React, { useState, useEffect } from 'react';
import { Checkbox, Container, FormControlLabel, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography, Skeleton } from '@mui/material';
import axios from 'axios';
import dayjs from 'dayjs';

const IncomePage = () => {

    const [regularIncomes, setRegularIncomes] = useState([]);
    const [groupedIncomes, setGroupedIncomes] = useState({});
    const [groupBySource, setGroupBySource] = useState(false);

    useEffect(() => {
        const loadIncomes = async () => {
            const { data } = await axios.get('/api/maasertracker/getallincomes');
            setRegularIncomes(data);
            const { data: groupedIncomes } = await axios.get('/api/maasertracker/getgroupedincomes');
            setGroupedIncomes(groupedIncomes);
        }
        loadIncomes();
    }, [])

    if (!regularIncomes[0]) {
        return (
            <Container maxWidth='sm' sx={{ flexDirection: 'column', alignItems: 'center', mt: 20 }}>
                <Skeleton />
                <Skeleton />
                <Skeleton />
            </Container>
        )
    }

    return (
        <Container sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', mt: 3 }}>
            <Typography variant="h2" gutterBottom component="div">
                Income History
            </Typography>

            <FormControlLabel
                control={
                    <Checkbox
                        checked={groupBySource}
                        onChange={(event) => setGroupBySource(event.target.checked)}
                        name="checkedB"
                        color="primary"
                    />
                }
                label="Group by source"
            />

            {!groupBySource ? (
                <TableContainer component={Paper} sx={{ maxWidth: '80%', width: '80%' }}>
                    <Table sx={{ minWidth: 650 }}>
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontSize: '18px', color:'blue' }}>Source</TableCell>
                                <TableCell align="right" sx={{ fontSize: '18px', color: 'blue' }}>Amount</TableCell>
                                <TableCell align="right" sx={{ fontSize: '18px', color: 'blue' }}>Date</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {regularIncomes.map((income) => (
                                <TableRow key={income.id}>
                                    <TableCell component="th" scope="row" sx={{ fontSize: '18px' }}>
                                        {income.sourceName}
                                    </TableCell>
                                    <TableCell align="right" sx={{ fontSize: '18px' }}>${income.amount}</TableCell>
                                    <TableCell align="right" sx={{ fontSize: '18px' }}>{dayjs(income.date).format('MM-DD-YYYY')}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            ) : (
                groupedIncomes.map((source) => (
                    <div key={source.sourceName} sx={{ width: '80%', maxWidth: '80%' }}>
                        <Typography variant="h5" gutterBottom component="div" sx={{ mt: 5 }}>
                            {source.sourceName}
                        </Typography>
                        <TableContainer component={Paper}>
                            <Table sx={{ minWidth: 650 }}>
                                <TableHead>
                                    <TableRow>
                                        <TableCell sx={{ fontSize: '18px', color: 'blue' }}>Source</TableCell>
                                        <TableCell align="right" sx={{ fontSize: '18px', color: 'blue' }}>Amount</TableCell>
                                        <TableCell align="right" sx={{ fontSize: '18px', color: 'blue' }}>Date</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {source.incomes.map((income) => (
                                        <TableRow key={income.id}>
                                            <TableCell component="th" scope="row" sx={{ fontSize: '18px' }}>
                                                {income.sourceName}
                                            </TableCell>
                                            <TableCell align="right" sx={{ fontSize: '18px' }}>${income.amount}</TableCell>
                                            <TableCell align="right" sx={{ fontSize: '18px' }}>{income.date}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </div>
                ))
            )}
        </Container>
    );
}

export default IncomePage;
