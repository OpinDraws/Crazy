using System.Numerics;
using MyVector;
namespace MyStack
{
    public class MyStack<T> : MyVector<T>
    {
        private MyVector<T> stack;
        private int top;
        public MyStack()
        {
            top = -1;
            stack = new MyVector<T>(1);
        }
        public void Push(T item)
        {
            top++;
            stack.Add(item);
        }
        public void Pop()
        {
            stack.Remove(top);
            top--;
        }
        public T Peek()
        {
            if (top == -1) throw new Exception("Stack is empty");
            else return stack.Get(top);
        }
        public bool Empty()
        {
            return stack.Count == 0;
        }
        public int Search(T item)
        {
            if (stack.IndexOf(item) == -1) return -1;
            return top - stack.IndexOf(item) + 1;
        }
        public int Top() { return top; }
        public void print()
        {
            for (int i = 0; i <= top; i++)
                Console.WriteLine(stack.Get(i));
        }
    }
}