using MyVector;
internal class Program
{

    static void Main()
    {
        MyStack<int> stack = new MyStack<int>();
    }
    public class MyStack<T> : MyVector<T>
    {
        MyVector<T> stack = new MyVector<T>();

        public void Push(T item)
        {
            stack.Add(item);

        }
        public T Pop()
        {
            int top = stack.Size() - 1;
            return RemoveReturn(top);
        }
        public T Peek()
        {
            int top = stack.Size();
            return stack.Get(top);
        }
        public bool Empty()
        {
            return stack.IsEmpty();
        }
        public int Search(T item)
        {
            if (stack.IndexOf(item) == -1) return -1;
            return stack.IndexOf(item) + 1;
        }
        
    }



}