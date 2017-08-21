import expect from 'expect';

import { range } from '../src/helpers/functional';

describe('Feature: range', () => {
    const Subject = (start, end) => {
        return range(start, end);
    };

    describe('Given a start of 0 and a count of 10', () => {
        const start = 0;
        const count = 10;

        describe('When executing range', () => {
            const result = Subject(start, count);

            it('should return an array with 10', () => {
                expect(result.length).toBe(10);
            });

            it('should have 0 indexed array', () => {
                expect(result[0]).toBe(0);
            });
        });
    });
});