using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2023
{
    class day17 : AOCPuzzle
    {
        
        
        long[,] neighbours = {
            {-1, 0},
            { 0, -1},
            { 0, 1},
            { 1, 0}
        };
        
        public day17() : base("17")
        {
            
        }

        public override void Part1(string[] input)
        {
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            Node current = new Node(0, 0, 0, '0', 0);
            Node end = new Node(matrix[0].Length - 1,  matrix.Length - 1);
            Node test = Dijkstra(current, end, matrix);

            PrintAnswer(test.cost);
        }

        private Node Dijkstra(Node start, Node end, char[][] matrix)
        {
            Queue<Node> q = new Queue<Node>();
            var seen = new List<Node>();
            Node current = start;
            q.Enqueue(start);
            
            while (q.Any())
            {
                q = new Queue<Node>(q.OrderBy(p => p.cost));
                
                current = q.Dequeue();

                if(current.x == end.x && end.y == current.y) break;
                if(seen.Contains(current)) continue;

                seen.Add(current);
                for (var i = 0; i < 4; i++)
                {
                    long x = current.x + neighbours[i, 0];
                    long y = current.y + neighbours[i, 1];
                    
                    if(x < 0 || y < 0 || y >= matrix.Length || x >= matrix[y].Length) continue;
                    Node other = null;
                    if(i == 0) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'L', 1);
                    if(i == 1) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'U', 1);
                    if(i == 2) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'D', 1);
                    if(i == 3) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'R', 1);

                    if (other.move == current.move)
                    {
                        other.consecutiveMoves += current.consecutiveMoves;
                    }

                    Node prev = current.prev;

                    while (prev != null)
                    {
                        if(prev.x == current.x && current.y == prev.y) break;
                        prev = prev.prev;
                    }
                    
                    if(seen.Contains(other) || other.consecutiveMoves >3 || prev != null) continue;
                    other.prev = current;
                    q.Enqueue(other);
                }
            }

            return current;
        }

        public override void Part2(string[] input)
        {
            var matrix = input.Select(s => s.ToCharArray()).ToArray();
            Node current = new Node(0, 0, 0, '0', 4);
            Node end = new Node(matrix[0].Length - 1,  matrix.Length - 1);
            Node test = Dijkstra2(current, end, matrix);
            
            PrintAnswer(test.cost);
        }
        
        private Node Dijkstra2(Node start, Node end, char[][] matrix)
        {
            Queue<Node> q = new Queue<Node>();
            var seen = new List<Node>();
            Node current = start;
            q.Enqueue(start);
            
            while (q.Any())
            {
                q = new Queue<Node>(q.OrderBy(p => p.cost));
                
                current = q.Dequeue();

                if(current.x == end.x && end.y == current.y)
                {
                    if(current.consecutiveMoves < 4) continue;
                    break;
                }
                if(seen.Contains(current)) continue;

                seen.Add(current);
                for (var i = 0; i < 4; i++)
                {
                    long x = current.x + neighbours[i, 0];
                    long y = current.y + neighbours[i, 1];
                    
                    if(x < 0 || y < 0 || y >= matrix.Length || x >= matrix[y].Length) continue;
                    Node other = null;
                    if(i == 0) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'L', 1);
                    if(i == 1) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'U', 1);
                    if(i == 2) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'D', 1);
                    if(i == 3) other = new Node(x, y, current.cost + (matrix[y][x] - '0'), 'R', 1);

                    if(current.move != other.move && current.consecutiveMoves < 4) continue;
                    if(current.move == other.move && current.consecutiveMoves >= 10) continue;
                                        
                    if (other.move == current.move)
                    {
                        other.consecutiveMoves += current.consecutiveMoves;
                    }

                    Node prev = current.prev;

                    while (prev != null)
                    {
                        if(prev.x == current.x && current.y == prev.y) break;
                        prev = prev.prev;
                    }
                    
                    if(prev != null) continue;
                    
                    other.prev = current;
                    q.Enqueue(other);
                }
            }

            return current;
        }
    }
}