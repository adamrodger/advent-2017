using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class Day02Tests
    {
        public static IEnumerable<object[]> Part1Data()
        {
            yield return new object[]
            {
                new string[]
                {
                    "5	1	9	5",
                    "7	5	3",
                    "2	4	6	8"
                },
                18
            };

            yield return new object[]
            {
                new string[]
                {
                    "493	458	321	120	49	432	433	92	54	452	41	461	388	409	263	58",
                    "961	98	518	188	958	114	1044	881	948	590	972	398	115	116	451	492",
                    "76	783	709	489	617	72	824	452	748	737	691	90	94	77	84	756",
                    "204	217	90	335	220	127	302	205	242	202	259	110	118	111	200	112",
                    "249	679	4015	106	3358	1642	228	4559	307	193	4407	3984	3546	2635	3858	924",
                    "1151	1060	2002	168	3635	3515	3158	141	4009	3725	996	142	3672	153	134	1438",
                    "95	600	1171	1896	174	1852	1616	928	79	1308	2016	88	80	1559	1183	107",
                    "187	567	432	553	69	38	131	166	93	132	498	153	441	451	172	575",
                    "216	599	480	208	224	240	349	593	516	450	385	188	482	461	635	220",
                    "788	1263	1119	1391	1464	179	1200	621	1304	55	700	1275	226	57	43	51",
                    "1571	58	1331	1253	60	1496	1261	1298	1500	1303	201	73	1023	582	69	339",
                    "80	438	467	512	381	74	259	73	88	448	386	509	346	61	447	435",
                    "215	679	117	645	137	426	195	619	268	223	792	200	720	260	303	603",
                    "631	481	185	135	665	641	492	408	164	132	478	188	444	378	633	516",
                    "1165	1119	194	280	223	1181	267	898	1108	124	618	1135	817	997	129	227",
                    "404	1757	358	2293	2626	87	613	95	1658	147	75	930	2394	2349	86	385"
                },
                21845
            };
        }

        public static IEnumerable<object[]> Part2Data()
        {
            yield return new object[]
            {
                new string[]
                {
                    "5	9	2	8",
                    "9	4	7	3",
                    "3	8	6	5"
                },
                9
            };

            yield return new object[]
            {
                new string[]
                {
                    "493	458	321	120	49	432	433	92	54	452	41	461	388	409	263	58",
                    "961	98	518	188	958	114	1044	881	948	590	972	398	115	116	451	492",
                    "76	783	709	489	617	72	824	452	748	737	691	90	94	77	84	756",
                    "204	217	90	335	220	127	302	205	242	202	259	110	118	111	200	112",
                    "249	679	4015	106	3358	1642	228	4559	307	193	4407	3984	3546	2635	3858	924",
                    "1151	1060	2002	168	3635	3515	3158	141	4009	3725	996	142	3672	153	134	1438",
                    "95	600	1171	1896	174	1852	1616	928	79	1308	2016	88	80	1559	1183	107",
                    "187	567	432	553	69	38	131	166	93	132	498	153	441	451	172	575",
                    "216	599	480	208	224	240	349	593	516	450	385	188	482	461	635	220",
                    "788	1263	1119	1391	1464	179	1200	621	1304	55	700	1275	226	57	43	51",
                    "1571	58	1331	1253	60	1496	1261	1298	1500	1303	201	73	1023	582	69	339",
                    "80	438	467	512	381	74	259	73	88	448	386	509	346	61	447	435",
                    "215	679	117	645	137	426	195	619	268	223	792	200	720	260	303	603",
                    "631	481	185	135	665	641	492	408	164	132	478	188	444	378	633	516",
                    "1165	1119	194	280	223	1181	267	898	1108	124	618	1135	817	997	129	227",
                    "404	1757	358	2293	2626	87	613	95	1658	147	75	930	2394	2349	86	385"
                },
                191
            };
        }

        [Theory]
        [MemberData(nameof(Part1Data))]
        public void Part1_WhenCalled_ProducesCorrectSolution(string[] input, int expected)
        {
            var solve = new Day02();

            int actual = solve.Part1(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Part2Data))]
        public void Part2_WhenCalled_ProducesCorrectSolution(string[] input, int expected)
        {
            var solve = new Day02();

            int actual = solve.Part2(input);

            Assert.Equal(expected, actual);
        }
    }
}