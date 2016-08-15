using System;
using System.IO;
using System.Linq;

namespace c2.tools.ExtTS
{
    using jsduck;
    public static class generator
    {


            //foreach (var moduleClass in Class.ModuleClasses)
            //{
            //    if (moduleClass.Key.Length > 0)
            //        writer.WriteLine($@"declare module {moduleClass.Key} {{");
            //    foreach (var cls in moduleClass.Value)
            //    {
            //        var indent = moduleClass.Key.Length > 0 ? tab : "";
            //        writer.WriteLine($@"{indent}{(moduleClass.Key.Length > 0 ? "export" : "declare")} class {cls.name.Substring(cls.name.LastIndexOf('.') + 1)}{((cls.singleton || String.IsNullOrEmpty(cls.extends)) ? "" : $@" extends {(Class.ClassMap.ContainsKey(cls.extends) ? Class.ClassMap[cls.extends].name : cls.extends)}")} {{");
            //        var indent2 = indent + tab;
            //        foreach (var member in cls.members.Where(m => m.owner == cls.name))
            //            WriteLines(writer, member.Sourcecode, indent2);
            //        writer.WriteLine($@"{indent}}}");
            //    }
            //    if (moduleClass.Key.Length > 0)
            //        writer.WriteLine($@"}}");
            //}

        //static void WriteLines(StreamWriter writer, string[] lines, string indent)
        //{
        //    foreach (var sourceline in lines)
        //    {
        //        if (sourceline.Length <= 0)
        //            writer.WriteLine();
        //        else
        //        {
        //            writer.Write(indent);
        //            writer.WriteLine(sourceline);
        //        }
        //    }
        //}
                                                            
        //// Whether the visibility rules say we should emit this member
        //static bool isMemberVisible(jsduck.Class cls, jsduck.Member member)
        //{
        //    return member.meta.@protected ? (!cls.singleton && !member.meta.@static) : !member.meta.@private;
        //}

        //// Test if one of the parent classes of the given class will emit the given member
        //static bool parentIncludesMember(jsduck.Class[] classes, jsduck.Class cls, string memberName, bool staticSide)
        //{
        //    if (String.IsNullOrEmpty(cls.extends))
        //        return false;

        //    var parentCls = lookupClass(classes, cls.extends);

        //    if (parentIncludesMember(classes, parentCls, memberName, staticSide)) {
        //        return true;
        //    }

        //    var member = lookupMember(parentCls.members, memberName, LookupMembers, staticSide);

        //    return member != null && isMemberVisible(parentCls, member);
        //}


        //// TODO: support closure syntax...
        //static string convertFromExtType(jsduck.Class[] classes, string senchaType, jsduck.Param[] properties = null)
        //{
        //    var subTypes = senchaType.replace(/ /g, '').split(/[|\/]/),
        //        mappedSubTypes = subTypes.map(function(t) { return mapSubType(t, subTypes.length > 1); });

        //    // any union type containing "any" is equivalent to "any"!
        //    for (var i=0; i<mappedSubTypes.length; i++) {
        //        if (mappedSubTypes[i] === 'any') {
        //            return 'any';
        //        }
        //    }

        //    return mappedSubTypes.join('|');
        //}

        //static string mapSubType(string typ, bool needsBracket)
        //{
        //    var arrays = /(\[])*$/.exec(typ)[0];

        //    if (arrays) {
        //        typ = typ.substring(0, typ.length - arrays.length);
        //    }

        //    if (typ === 'Function' && properties) {

        //        // if no return type is specified, assume any - it is not safe to assume void
        //        var params = [],
        //            retTyp = 'any';

        //        properties.forEach(function(property) {
        //            if (property.name === 'return') {
        //                retTyp = convertFromExtType(classes, property.type, property.properties);
        //            }
        //            else {
        //                var opt = property.optional ? '?: ' : ': ',
        //                    typ = convertFromExtType(classes, property.type, property.properties);
        //                params.push(property.name + opt + typ);
        //            }
        //        });

        //        var fnType = '(' + params.join(', ') + ') => ' + retTyp;
        //        return (needsBracket || arrays ? ('(' + fnType + ')') : fnType) + arrays;
        //    }
        //    else if (SPECIAL_CASE_TYPE_MAPPINGS.hasOwnProperty(typ)) {
        //        return SPECIAL_CASE_TYPE_MAPPINGS[typ] + arrays;
        //    }
        //    else {
        //        try {
        //            var cls = lookupClass(classes, typ);
        //        }
        //        catch (e) {
        //            Console.WriteLine($@"Warning: unable to find class, using 'any' instead: '{typ}'");
        //            return 'any';
        //        }
        //        // enum types (e.g. Ext.enums.Widget) need special handling
        //        return (cls.enum ? convertFromExtType(classes, cls.enum.type) : cls.name) + arrays;
        //    }
        //}
    }
}
