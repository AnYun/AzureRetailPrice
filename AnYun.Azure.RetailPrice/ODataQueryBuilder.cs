using AnYun.Azure.RetailPrice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnYun.Azure.RetailPrice
{
    public static class ODataQueryBuilder
    {
        public static string ToODataFilter(this IQueryable<AzureRetailPriceQuery> query)
        {
            var expression = query.Expression;
            var visitor = new ODataExpressionVisitor();
            visitor.Visit(expression);
            return visitor.Filter;
        }

        private class ODataExpressionVisitor : ExpressionVisitor
        {
            private StringBuilder _filterBuilder = new StringBuilder();

            public string Filter => _filterBuilder.ToString();

            protected override Expression VisitBinary(BinaryExpression node)
            {
                _filterBuilder.Append("(");
                Visit(node.Left);

                switch (node.NodeType)
                {
                    case ExpressionType.Equal:
                        _filterBuilder.Append(" eq ");
                        break;
                    case ExpressionType.NotEqual:
                        _filterBuilder.Append(" ne ");
                        break;
                    case ExpressionType.GreaterThan:
                        _filterBuilder.Append(" gt ");
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        _filterBuilder.Append(" ge ");
                        break;
                    case ExpressionType.LessThan:
                        _filterBuilder.Append(" lt ");
                        break;
                    case ExpressionType.LessThanOrEqual:
                        _filterBuilder.Append(" le ");
                        break;
                    case ExpressionType.AndAlso:
                        _filterBuilder.Append(" and ");
                        break;
                    case ExpressionType.OrElse:
                        _filterBuilder.Append(" or ");
                        break;
                    default:
                        throw new NotSupportedException($"The binary operator '{node.NodeType}' is not supported");
                }

                Visit(node.Right);
                _filterBuilder.Append(")");
                return node;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter)
                {
                    _filterBuilder.Append(node.Member.Name);
                }
                else
                {
                    throw new NotSupportedException($"The member '{node.Member.Name}' is not supported");
                }
                return node;
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (node.Type == typeof(string))
                {
                    //_filterBuilder.Append($"'{node.Value}'");
                    _filterBuilder.Append($"'{WebUtility.UrlEncode(node.Value.ToString())}'");
                }
                else if (node.Type.IsGenericType && node.Type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var list = (System.Collections.IList)node.Value;
                    _filterBuilder.Append("(");
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i > 0)
                        {
                            _filterBuilder.Append(", ");
                        }
                        _filterBuilder.Append(list[i]);
                    }
                    _filterBuilder.Append(")");
                }
                else
                {
                    //_filterBuilder.Append(node.Value);
                }
                return node;
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node.Method.Name == "Contains" && node.Object != null && node.Object.Type == typeof(string))
                {
                    _filterBuilder.Append("contains(");
                    Visit(node.Object);
                    _filterBuilder.Append(",");
                    Visit(node.Arguments[0]);
                    _filterBuilder.Append(")");
                    return node;
                }
                return base.VisitMethodCall(node);
            }
        }
    }
}
